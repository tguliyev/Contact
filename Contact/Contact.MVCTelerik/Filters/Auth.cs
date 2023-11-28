using System;
using Contact.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Contact.MVC.Filters
{
    /// <summary>
    /// Custom Auth filter attribute , checks JWT token which comes from Cookie
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Auth : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("Token", out string? value);

            var fullToken = value?.ToString();

            if (string.IsNullOrEmpty(fullToken))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            string tokenOnly = fullToken;
            var claims = GetPrincipalFromToken(tokenOnly, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"])),
                ValidIssuer = config["JWTSettings:Issuer"],
                ValidAudiences = new List<string> { config["JWTSettings:Audience"] },
                ValidateIssuer = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = false
            });

            if (claims == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

        }
        private ClaimsPrincipal GetPrincipalFromToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch { return null; }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        =>
            (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                StringComparison.InvariantCultureIgnoreCase);
    }

}

