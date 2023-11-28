using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.xUnit
{
    public class TestCredentials
    {
        public const string LoginEndpoint = "/api/account/login";
        public const string CreateUserContactEndPoint = "/api/users/contacts";
        public const string UpdateUserContactEndPoint = "/api/users/contacts/";
        public const string DeleteUserContactEndPoint = "/api/users/contacts/";

        public static LoginRequest GetJWTTokenRequestParameters()
        {
            return new LoginRequest
            {
                Username = "turalguliyev",
                Password = "Tural123@"
            };
        }

        public static CreateUserContactRequest GetCreateUserContactRequestParameters()
        {
            return new CreateUserContactRequest
            {
                Name = "Test",
                Surname = "Test",
                Phone = Guid.NewGuid().ToString(),
                Email = "test.test@test.com"
            };
        }

        public static UpdateUserContactRequest GetUpdateUserContactRequestParameters()
        {
            return new UpdateUserContactRequest
            {
                Name = "Test",
                Surname = "Test",
                Phone = "test",
                Email = "test.test@test.com"
            };
        }

    }
}
