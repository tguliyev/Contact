using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contact.Application.CQRS.Core;
using Contact.Application.Models.Response;
using Contact.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IConfiguration? _configuration;

        public IConfiguration? Configuration => _configuration ?? HttpContext.RequestServices.GetService<IConfiguration>();



        /// <summary>
        /// In order to share session from classic asp and asp net core return response via cookie
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public ActionResult<ApiResult<LoginResponse>> ResponseViaCookie(ApiResult<LoginResponse> data)
        {
            if (data.StatusCode != (int)HttpStatusCode.OK)
            {
                return ApiResult<LoginResponse>.Error(ErrorCodes.USER_IS_NOT_EXISTS);
            }
            var cookie = new CookieOptions()
            {
                Domain = Configuration["CookieSettings:Domain"],
                Path = "/",
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddHours(Convert.ToInt32(5))
            };

            Response.Cookies.Append("Token", data.Response.Token, cookie);
            return data;

        }


        protected int GetUser()
        {
            string? userIdValue = HttpContext.User.Claims.Where(c => c.Type == "id").Select(c => c.Value).FirstOrDefault();

            return userIdValue == null ? 0 : Convert.ToInt32(userIdValue);
        }
    }
}

