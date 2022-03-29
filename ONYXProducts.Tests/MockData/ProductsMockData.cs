using ONYXProducts.Catalog.Adapters.Persistence;
using ONYXProducts.Domain;
using ONYXProducts.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ONYXProducts.WebApi.Tests.MockData
{
    public class ProductsMockData
    {
        internal static ICollection<IProduct> GetMockedProducts()
        {
            return new List<IProduct> {
                new Product{
                     Id = Guid.NewGuid(),
                      Colour = "yellow"
                },
                new Product{
                     Id = Guid.NewGuid(),
                      Colour = "blue"
                },
                new Product{
                     Id = Guid.NewGuid(),
                      Colour = "blue"
                }

            };
        }

        internal static IEnumerable<IProduct> GetEmptyProductsList()
        {
            return new List<IProduct>();
        }

        internal static IEnumerable<IProduct> GetMultipleProductsByColour()
        {
            return GetMockedProducts().Where(p => p.Colour == "blue");
        }

        internal static string GetValidToken()
        {
            return "VALIDTOKEN";
        }

        internal static ICollection<ProductDbItem> ProductDbItems()
        {
            return new List<ProductDbItem> {
                new ProductDbItem{ Id = Guid.NewGuid(), Colour="yellow", Price=10},
                new ProductDbItem{ Id = Guid.NewGuid(), Colour="blue", Price=12},
                new ProductDbItem { Id = Guid.NewGuid(), Colour = "blue", Price = 13 },
                new ProductDbItem { Id = Guid.NewGuid(), Colour = "red", Price = 5 },
            };

        }
    }
}
