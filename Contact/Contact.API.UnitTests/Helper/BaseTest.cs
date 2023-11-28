using System;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;

namespace Contact.API.UnitTests.Helper
{
    public class BaseTest
    {
        protected readonly IAccountService _accountService;
        protected readonly IUserContactService _userContactService;
        protected int UserId;
        protected int UserContactId;
        public BaseTest()
        {
            _accountService = ServiceHelper.GetRequiredService<IAccountService>() ?? throw new ArgumentNullException(nameof(IAccountService));


            _userContactService = ServiceHelper.GetRequiredService<IUserContactService>() ?? throw new ArgumentNullException(nameof(IUserContactService));


            AddDefaultUserForTesting().Wait();
            AddDefaultUserContactForTesting().Wait();
        }

        private async Task AddDefaultUserForTesting()
        {
            var model = new RegisterRequest();

            model.Name = "Tural";
            model.Surname = "Guliyev";
            model.Email = "tguliev2124@gmail.com";
            model.Password = "admin";
            model.Username = "turalguliyev";

            var response = await _accountService.Register(model);

        }

        private async Task AddDefaultUserContactForTesting()
        {
            var userContact = new CreateUserContactRequest();
            userContact.Email = "elnur@gmail.com";
            userContact.Name = "Elnur";
            userContact.Surname = "Huseynov";
            userContact.Phone = "0506673475";


            await _userContactService.CreateUserContact(userContact, UserId);
        }
    }
}

