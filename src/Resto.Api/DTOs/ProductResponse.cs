using Resto.Api.Models;

namespace RestoApi.DTOs;

public sealed record ProductResponse(
    int Id,
    int CategoryId,
    string Name,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
)
{
    public static ProductResponse FromModel(Product product) => new(
        product.Id,
        product.CategoryId,
        product.Name,
        product.Price,
        product.Stock,
        product.IsActive,
        product.CreatedAt,
        product.UpdatedAt
    );
}