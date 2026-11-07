
using Resto.Api.DTOs;
using Resto.Api.Models;
using Resto.Api.Repositories;
using RestoApi.DTOs;

namespace Resto.Api.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository=repository;
    }
    public async Task<ProductResponse?> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            CategoryId = request.CategoryId, 
            Name = request.Name.Trim(),
            Price = request.Price,
            Stock = request.Stock,
            IsActive = request.IsActive
        };

        var created = await _repository.TryAddAsync(product, cancellationToken);

        if (!created)
        {
            return null;
        }

        return created ? ProductResponse.FromModel(product) : null;

    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Task.FromResult(false);
        }
        return _repository.TryDeleteAsync(id,cancellationToken);
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

        return products.Select(ProductResponse.FromModel).ToArray();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return null;
        }

        var product = await _repository.GetByIdAsync(id, cancellationToken);

        return product is null ? null : ProductResponse.FromModel(product);
        // throw new NotImplementedException();
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return null;
        }
        var product = new Product
        {
            Id = id,
            CategoryId = request.CategoryId,
            Name = request.Name.Trim(),
            Price = request.Price,
            Stock = request.Stock,
            IsActive = request.IsActive
        };

        var updated = await _repository.TryUpdateAsync(product, cancellationToken);

        return updated ? ProductResponse.FromModel(product) : null;
    }
}