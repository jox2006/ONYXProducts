using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ONYX.Products.Catalog.Adapters;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Application.UseCases.UserAuthentication;
using ONYXProducts.WebApi.Tests.MockData;
using System.Threading.Tasks;
using Xunit;

namespace ONYXProducts.WebApi.Tests.Systems.Controllers
{
    public class TestProductController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn_200Status()
        {

            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();
            productCatalogService.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(ProductsMockData.GetMockedProducts());

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.GetAllAsync();

            ///Assert
            (result.Result as OkObjectResult).Should().NotBeNull();
            (result.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_204Status()
        {

            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();
            productCatalogService.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(ProductsMockData.GetEmptyProductsList());

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.GetAllAsync();

            ///Assert
            (result.Result as OkObjectResult)?.StatusCode.Should().Be(204);

        }
        [Fact]
        public async Task GetByColourAsync_ShouldReturn_200tatus()
        {

            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();
            productCatalogService.Setup(p => p.GetAllProductsByColourAsync(It.IsAny<string>())).ReturnsAsync(ProductsMockData.GetMultipleProductsByColour());

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.GetAllAsync();

            ///Assert
            (result.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task GetByColourAsync_ShouldReturn_204Status()
        {

            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();
            productCatalogService.Setup(p => p.GetAllProductsByColourAsync(It.IsAny<string>())).ReturnsAsync(ProductsMockData.GetEmptyProductsList());

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.GetAllAsync();

            ///Assert
            (result.Result as OkObjectResult)?.StatusCode.Should().Be(204);

        }

        [Fact]
        public async Task Authenticate_ShouldReturn_200tatus()
        {

            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();
            var credentials = new Mock<UserCredential>();

            jwtAuthenticator.Setup(p => p.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ProductsMockData.GetValidToken());

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.Authenticate(credentials.Object);

            ///Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Authenticate_ShouldReturn_204Status()
        {

            ///Arrange
            ///Arrange
            var productCatalogService = new Mock<IProductsCatalog>();

            var jwtAuthenticator = new Mock<IJwtONYXAuthenticator>();
            var credentials = new Mock<UserCredential>();

            jwtAuthenticator.Setup(p => p.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(string.Empty);

            var sut = new ProductsController(productCatalogService.Object, jwtAuthenticator.Object);
            ///Act

            var result = await sut.Authenticate(credentials.Object);

            ///Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(401);

        }

    }
}
