namespace ONYXProducts.Catalog.Adapters.Persistence
{
    /// <summary>
    /// Our database item that may have different structure that the Product managed at Domain level.
    /// </summary>
    public class ProductDbItem
    {
        public Guid Id;
        public string? Colour;
        public decimal Price;
    }
}