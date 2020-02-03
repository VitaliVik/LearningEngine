using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace LearningEngine.ApiTests.Tests
{

    public class RegisterUserTest: IClassFixture<CustomWebApplicationFactory<Api.Startup>>
    {
        private readonly CustomWebApplicationFactory<Api.Startup> _factory;
        private MultipartFormDataContent UserData { get; set; }
        public RegisterUserTest(CustomWebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            MultipartFormDataContent fc = new MultipartFormDataContent();
            fc.Add(new StringContent("kekov"), "username");
            fc.Add(new StringContent("veko@mail.com"), "email");
            fc.Add(new StringContent("password123"), "password");
            UserData = fc;
        }

        [Theory]
        [InlineData("/api/account/register")]
        public async Task RegisterNewUserTest(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync(url, UserData);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            response = await client.PostAsync(url, UserData);
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


        }

    }
}
