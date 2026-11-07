using Resto.Api.DTOs;
using RestoApi.DTOs;

namespace Resto.Api.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponse?> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}