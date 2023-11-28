using System;
using Contact.API.UnitTests.Helper;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;

namespace Contact.API.UnitTests.Tests
{
    public class AccountServiceTests : BaseTest
    {

        [Fact]
        public async Task RegisterUserWithCorrectCredentials_ReturnUserId()
        {
            var model = new RegisterRequest();

            model.Name = "Tural";
            model.Surname = "Guliyev";
            model.Email = "tguliev9@gmail.com";
            model.Password = "admin";
            model.Username = "admin";

            var response = await _accountService.Register(model);

            Assert.NotNull(response.Response);
        }

        [Fact]
        public async Task IFUsernameExistsThen_ReturnError()
        {
            var model = new RegisterRequest();

            model.Name = "Tural";
            model.Surname = "Guliyev";
            model.Email = "tguliev2124@gmail.com";
            model.Password = "admin";
            model.Username = "turalguliyev";

            var response = await _accountService.Register(model);

            Assert.Equal(101, response.ErrorCode);
        }

    }
}

