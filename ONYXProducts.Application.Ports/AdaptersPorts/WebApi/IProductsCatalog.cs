using ONYXProducts.Domain.Interfaces;

namespace ONYXProducts.Application.AdaptersPorts.WebApi
{
    /// <summary>
    /// This is the interface  WEB - APPLICATION => Input Port at Application level in Clean Architecture
    /// </summary>
    public interface IProductsCatalog
    {
        Task<IEnumerable<IProduct>> GetAllProductsAsync();
        Task<IEnumerable<IProduct>> GetAllProductsByColourAsync(string colour);
    }
}
