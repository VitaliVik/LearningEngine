using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LearningEngine.IntegrationTests.Tests
{
    public class AccountTest: IClassFixture<CustomWebApplicationFactory<LearningEngine.Api.Startup>>
    {
        private readonly CustomWebApplicationFactory<LearningEngine.Api.Startup> _factory;

        public AccountTest(CustomWebApplicationFactory<LearningEngine.Api.Startup> factory)
        {
            _factory = factory;
        }
       
        [Theory]
        [InlineData("/api/account/register")]
        public async Task RegisterNewUserTest(string url)
        {
            var client = _factory.CreateClient();
            MultipartFormDataContent fc = new MultipartFormDataContent();
            fc.Add(new StringContent("kekov"), "username");
            fc.Add(new StringContent("veko@mail.com"), "email");
            fc.Add(new StringContent("password123"), "password");

            var response = await client.PostAsync(url, fc);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            response = await client.PostAsync(url, fc);
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
