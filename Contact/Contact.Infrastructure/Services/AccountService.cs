using Azure;
using Contact.Application.CQRS.Core;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.Domain.Entities;
using Contact.Domain.Enums;
using Contact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtService;

        public AccountService(ApplicationDbContext context,
                              IConfiguration configuration,
                              IJWTService jWTService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jWTService;
        }

        public async Task<ApiResult<CheckTokenResponse>> CheckToken(string token)
        {
            //Check token validaty and return result
            var result = _jwtService.ValidateJwtToken(token);


            return ApiResult<CheckTokenResponse>.OK(new CheckTokenResponse
            {
                Token = result,

            });


        }

        public async Task<ApiResult<LoginResponse>> Login(LoginRequest request)
        {

            //get user from database with email
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == request.Email);

            //if we dont have a username in database then error
            if (user == null)
                return ApiResult<LoginResponse>.Error(ErrorCodes.EMAIL_OR_PASSWORD_IS_NOT_CORRECT);


            //checking password with our hash if doesnt match then error
            if (!user.CheckPassword(request.Password))
                return ApiResult<LoginResponse>.Error(ErrorCodes.EMAIL_OR_PASSWORD_IS_NOT_CORRECT);



            //generating JWT token , use jwtService for this
            string token = _jwtService.GenerateJwtToken(user);

            var response = new LoginResponse
            {
                Token = token
            };

            return ApiResult<LoginResponse>.OK(response);
        }

        public async Task<ApiResult<RegisterResponse>> Register(RegisterRequest request)
        {

            //get user from database with email
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == request.Email);


            //if we have a user in database then error
            if (user != null)
                return ApiResult<RegisterResponse>.Error(ErrorCodes.USER_IS_ALREADY_EXISTS_WITH_THIS_EMAIL);


            //creating new user with correct credentials
            user = new User(request.Name,
                               request.Surname,
                               request.Email,
                               request.Username);


            //making password with hmacsha256
            user.AddPassword(request.Password);


            //adding it to database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();


            var response = new RegisterResponse
            {
                Name = user.Name,
                Surname = user.Surname,
                UserId = user.Id,
                Username = user.Username
            };

            return ApiResult<RegisterResponse>.OK(response);
        }
    }
}
