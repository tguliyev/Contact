using Contact.Application.CQRS.Core;
using Contact.Application.Models.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contact.API.xUnit
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Program>();
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJWTAsync());
        }

        private async Task<string> GetJWTAsync()
        {
           
                var response = await TestClient.PostAsJsonAsync
                    (
                    TestCredentials.LoginEndpoint,
                    TestCredentials.GetJWTTokenRequestParameters()).Result.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResult<LoginResponse>>(response);

                return result.Response.Token;
        }
    }
}
