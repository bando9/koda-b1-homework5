using Resto.Api.Models;

namespace Resto.Api.DTOs;

public sealed record ProductResponse(
    int CategoryId,
    string ProductCode,
    string Name,
    decimal Price,
    int Stock
)
{
    public static ProductResponse FromModel(Product product) => new(
        product.CategoryId,
        product.ProductCode,
        product.Name,
        product.Price,
        product.Stock
    );
}