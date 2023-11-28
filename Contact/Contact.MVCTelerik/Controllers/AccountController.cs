using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contact.Application.CQRS.Core;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //make request to login api
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);

                var response = await client.PostAsync("account/login", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                string apiResponse = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonConvert.DeserializeObject<ApiResult<LoginResponse>>(apiResponse);

                //if there is a problem then error
                if (loginResponse.StatusCode != (int)HttpStatusCode.OK)
                {
                    return View(request);
                }

                //add Token param to cookie
                Response.Cookies.Append("Token", loginResponse.Response.Token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Domain = _configuration["CookieSettings:Domain"],
                    Expires = DateTime.UtcNow.AddHours(5),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None
                });

                //make cookie authentication
                var claims = new List<Claim>
                  {
                      new Claim("jwt_token",loginResponse.Response.Token ),
                  };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //make request to register api
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["API:BaseUrl"]);

                var response = await client.PostAsync("account/register", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                string apiResponse = await response.Content.ReadAsStringAsync();

                var registerResponse = JsonConvert.DeserializeObject<ApiResult<LoginResponse>>(apiResponse);

                //if there is a problem then error
                if (registerResponse?.StatusCode != (int)HttpStatusCode.OK)
                    return View(request);

                //if okay then redirect to login
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //delete cookies
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index", "Home");
        }
    }
}

