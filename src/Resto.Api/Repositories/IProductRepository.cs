using Resto.Api.Models;

namespace Resto.Api.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> TryAddAsync(Product product, CancellationToken cancellationToken);
    Task<bool> TryUpdateAsync(Product product, CancellationToken cancellationToken);

    Task<bool> TryDeleteAsync(int id, CancellationToken cancellationToken);

}