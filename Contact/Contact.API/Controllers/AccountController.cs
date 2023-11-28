using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Application.CQRS.Core;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<LoginResponse>>> Login(LoginRequest request)
            => ResponseViaCookie(await _accountService.Login(request));

        [HttpPost("register")]
        public async Task<ActionResult<ApiResult<RegisterResponse>>>
            Register(RegisterRequest request)
            => await _accountService.Register(request);

        [HttpGet("check-token")]
        public async Task<ActionResult<ApiResult<CheckTokenResponse>>> CheckToken()
        {
            string token = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "Token").Value;
            if (string.IsNullOrEmpty(token))
                return ApiResult<CheckTokenResponse>.Error(ErrorCodes.AUTH_TOKEN_IS_EMPTY);

            return await _accountService.CheckToken(token);
        }
    }
}

