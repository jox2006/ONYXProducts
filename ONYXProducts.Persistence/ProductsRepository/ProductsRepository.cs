using ONYXProducts.Application.AdaptersPorts.Persistence;
using ONYXProducts.Domain;
using ONYXProducts.Domain.Interfaces;


namespace ONYXProducts.Catalog.Adapters.Persistence
{
    /// <summary>
    /// Our implementation of the interface at ADAPTER PERSISTANCE layer level.
    /// </summary>
    public class ProductsRepository : IRepository<IProduct>
    {

        private readonly ICollection<ProductDbItem> dbList = new List<ProductDbItem>();
        public ProductsRepository()
        {
            dbList = new List<ProductDbItem> {
                new ProductDbItem{ Id = Guid.NewGuid(), Colour="yellow", Price=10},
                new ProductDbItem{ Id = Guid.NewGuid(), Colour="blue", Price=12},
                new ProductDbItem { Id = Guid.NewGuid(), Colour = "blue", Price = 13 },
                new ProductDbItem { Id = Guid.NewGuid(), Colour = "red", Price = 5 },
            };
        }
        public ProductsRepository(ICollection<ProductDbItem> dbList) // I will use this workaround for unit testing as I am not implementing a db for this test for simplicity
        {
            this.dbList = dbList;

        }

        public Task<IEnumerable<IProduct>> GetAllAsync()
        {
            //This should be an async call to a database in a real scenario
            return Task.FromResult(dbList.Select(p => ConvertToProduct(p)).AsEnumerable());
        }

        public Task<IEnumerable<IProduct>> GetAllAsync(Func<IProduct, bool> filter)
        {

            //This should be an async call to a database in a real scenario
            var results = dbList.Where(p => filter(ConvertToProduct(p))).Select(p => ConvertToProduct(p)).AsEnumerable();
            return Task.FromResult(results);
        }

        private static IProduct ConvertToProduct(ProductDbItem product)
        {
            return new Product
            {
                Id = product.Id,
                Colour = product.Colour ?? String.Empty
            };
        }
    }
}
