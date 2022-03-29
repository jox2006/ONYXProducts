using ONYXProducts.Domain.Interfaces;

namespace ONYXProducts.Domain
{
    /// <summary>
    ///Product entity at DOMAIN Layer level in Clean Architecture.
    /// </summary>
    public class Product : IProduct
    {
        //Properties
        public Guid Id { get; set; }
        public string Colour { get; set; } = string.Empty;

        //Validation
        public bool HasValidColor => Colour != string.Empty;

    }
}