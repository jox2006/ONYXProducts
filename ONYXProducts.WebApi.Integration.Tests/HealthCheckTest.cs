using Microsoft.AspNetCore.Mvc.Testing;
using ONYX.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ONYXProducts.WebApi.Integration.Tests
{
    public class HealthCheckTest
    {
        private HttpClient _client;

        public HealthCheckTest()
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_Returns_string()
        {

            ///Arrange
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateDefaultClient();

            ///Act
            var result = await _client.GetAsync(ApiRoutes.HealthCheck);
            var stringResult = await result.Content.ReadAsStringAsync();

            ///Assert 
            Assert.Equal("Healthy", stringResult);  //The memory used is expected to be more than 1000 bytes! :-)

        }
    }
}