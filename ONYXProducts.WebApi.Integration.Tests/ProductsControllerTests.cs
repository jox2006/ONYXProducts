using FluentAssertions;
using ONYX.Configuration;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ONYXProducts.WebApi.Integration.Tests
{
    public class ProductsControllerTests : BaseIntegrationTest
    {
        [Fact]
        public async Task GetAllProducts_when_autothenticated_return_products()
        {
            ///Arrange
            ///
            await AuthenticateAsync();

            ///Act
            var response = await _httpClient.GetAsync(ApiRoutes.GetAll);
            var result =await response.Content.ReadFromJsonAsync<List<ProductWebDto>>();

            ///Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeNull();
            Assert.True(result.Count == 4);

        }
        [Fact]
        public async Task GetAllProducts_when_NOT_autothenticated_return_401_Unauthorized()
        {
            ///Arrange

            ///Act
            var response = await _httpClient.GetAsync(ApiRoutes.GetAll);

            ///Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task GetProductsByColour_when_autothenticated_return_productsofthesamecolour()
        {
            ///Arrange
            ///
            await AuthenticateAsync();

            ///Act
            var response = await _httpClient.GetAsync(ApiRoutes.GetByColour.Replace("{colour}", "blue"));
            var result = await response.Content.ReadFromJsonAsync<List<ProductWebDto>>();

            ///Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(result.Count == 2);

        }


    }
}