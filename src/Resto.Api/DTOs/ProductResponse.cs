using Resto.Api.Models;

namespace RestoApi.DTOs;

public sealed record ProductResponse(
    int Id,
    int CategoryId,
    string CategoryName,
    string Name,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
)
{
    public static ProductResponse FromModel(Product product, string categoryName = "") => new(
        product.Id,
        product.CategoryId,
        categoryName,
        product.Name,
        product.Price,
        product.Stock,
        product.IsActive,
        product.CreatedAt,
        product.UpdatedAt
    );
}