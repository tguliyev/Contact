using Contact.API.UnitTests.Helper;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;
using Contact.Domain.Entities;

namespace Contact.API.UnitTests.Tests;


public class UserContactServiceTests : BaseTest
{

    [Fact]
    public async Task AddUserContactWithCorrectCredentials_ReturnUserContactId()
    {
        var userContact = new CreateUserContactRequest();
        userContact.Email = "test@mail.com";
        userContact.Name = "Tural";
        userContact.Surname = "Guliyev";
        userContact.Phone = "661759635";


        var response = await _userContactService.CreateUserContact(userContact, UserId);

        UserContactId = response.Response.ContactId;

        Assert.NotNull(response.Response);
    }

    [Fact]
    public async Task EditUserContactWithCorrectCredentials_ReturnUserContactId()
    {
        var userContact = new UpdateUserContactRequest();
        userContact.Email = "test@mail.com";
        userContact.Name = "TuralChanged";
        userContact.Surname = "GuliyevChanged";
        userContact.Phone = "+34 661 759 635";
        userContact.Address = "address";

        var response = await _userContactService.UpdateUserContact(userContact, UserContactId, UserId);

        Assert.NotNull(response.Response);

    }

}
