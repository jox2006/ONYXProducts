using FluentAssertions;
using ONYXProducts.Catalog.Adapters.Persistence;
using ONYXProducts.Domain.Interfaces;
using ONYXProducts.WebApi.Tests.MockData;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ONYXProducts.WebApi.Tests.Systems.Services
{
    public class TestProductsRepository
    {
        [Fact]
        public async Task ProductsRepository_must_return_ListOfProducts_when_ThereAreItemsInTheCollection()
        {

            ///Arrange
            var dbItems = ProductsMockData.ProductDbItems();
            var sut = new ProductsRepository(dbItems);

            ///Act
            var products = await sut.GetAllAsync();

            ///Asserts
            products.Should().HaveCount(dbItems.Count);
            products.Should().BeAssignableTo<IEnumerable<IProduct>>();

        }
        [Theory]
        [InlineData(new object[] { "blue", 2 })]
        [InlineData(new object[] { "red", 1 })]
        [InlineData(new object[] { "yellow", 1 })]
        public async Task ProductsRepository_must_return_ListOfProducts_when_ApplyingFilterExpression(string colour, int expectedNumber)
        {

            ///Arrange
            var dbItems = ProductsMockData.ProductDbItems();
            var sut = new ProductsRepository(dbItems);
            var filter = (IProduct p) => p.Colour == colour;

            ///Act
            var products = await sut.GetAllAsync(filter);

            ///Asserts
            products.Should().HaveCount(expectedNumber);
            products.Should().BeAssignableTo<IEnumerable<IProduct>>();

        }

    }
}
