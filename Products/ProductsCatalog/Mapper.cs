using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Domain.Interfaces;

namespace ONYXProducts.WebApi.ProductsCatalog
{
    public static class Mapper
    {
        public static ProductWebDto ToProductWebDto(IProduct product)
        {

            return new ProductWebDto()
            {
                Colour = product.Colour,
                Id = product.Id.ToString(),
            };

        }
    }
}
