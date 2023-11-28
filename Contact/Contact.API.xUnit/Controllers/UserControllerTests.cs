using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.API;
using Contact.Application.CQRS.Core;

namespace Contact.API.xUnit.Controllers
{
    public class UserControllerTests : IntegrationTestBase
    {


        [Fact]
        public async Task CreateUserContact_ReturnsContactId()
        {

            await AuthenticateAsync();



            var response = await TestClient.PostAsJsonAsync(
                        TestCredentials.CreateUserContactEndPoint,
                        TestCredentials.GetCreateUserContactRequestParameters());


            response.EnsureSuccessStatusCode();
            var createdResponse = await response.Content.ReadFromJsonAsync<ApiResult<CreateUserContactResponse>>();
            Assert.NotNull(createdResponse);
            Assert.NotEqual(0, createdResponse.Response.ContactId);
        }

        [Fact]
        public async Task UpdateUserContact_ReturnsContactId()
        {
            await AuthenticateAsync();



            var response = await TestClient.PostAsJsonAsync(
                        TestCredentials.UpdateUserContactEndPoint,
                        TestCredentials.GetUpdateUserContactRequestParameters());


            response.EnsureSuccessStatusCode();
            var createdResponse = await response.Content.ReadFromJsonAsync<ApiResult<UpdateUserContactResponse>>();
            Assert.NotNull(createdResponse);
            Assert.NotEqual(0, createdResponse.Response.ContactId);
        }


    }
}