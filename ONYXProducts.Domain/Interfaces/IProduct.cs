
namespace ONYXProducts.Domain.Interfaces
{
    public interface IProduct
    {
        string Colour { get; set; }
        bool HasValidColor { get; }
        Guid Id { get; set; }
    }
}