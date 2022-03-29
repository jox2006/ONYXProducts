using ONYXProducts.Application.AdaptersPorts.Persistence;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Domain.Interfaces;

namespace ONYXProducts.Catalog.Application.Services
{
    /// <summary>
    /// This service will implement our use casese related with Products
    /// </summary>
    public class ProductCatalog : IProductsCatalog
    {
        private readonly IRepository<IProduct> _repository;

        public ProductCatalog(IRepository<IProduct> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<IProduct>> GetAllProductsAsync()
        {
            var allProducts = await _repository.GetAllAsync();
            return allProducts.AsEnumerable();
        }

        public async Task<IEnumerable<IProduct>> GetAllProductsByColourAsync(string colour)
        {
            var allProducts = await _repository.GetAllAsync(x => x.Colour == colour);
            return allProducts.AsEnumerable();
        }

    }
}
