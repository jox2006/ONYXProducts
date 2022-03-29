using Microsoft.AspNetCore.Mvc.Testing;
using ONYXProducts.Application.UseCases;
using System.Net.Http;
using System.Threading.Tasks;

namespace ONYXProducts.WebApi.Integration.Tests
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient _httpClient;

        public BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>();

            _httpClient = appFactory.CreateDefaultClient();

        }

        protected async Task AuthenticateAsync()
        {
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
        }

        private async Task<string?> GetTokenAsync()
        {
            var tokenGenerator = new JwtONYXAuthenticator("This encryption key should be saved in the config, or within a secrets");
            //var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Authenticate, new UserCredential {
            //    userName = "UserName1"
            //    password = "password1"
            //})

            return await tokenGenerator.AuthenticateAsync("UserName1", "password1"); // This user exists in our repository
        }
    }
}