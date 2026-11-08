using Microsoft.AspNetCore.Mvc;
using Resto.Api.DTOs;
using Resto.Api.Services;

namespace Resto.Api.Controllers;

[ApiController]
[Route("api/v1/products")]

public sealed class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }


    // *Get ALl Products
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }


    // *Get product by Id
    [HttpGet("{productCode}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetByCode([FromRoute] string productCode, CancellationToken cancellationToken)
    {
        var product = await _service.GetByCodeAsync(productCode, cancellationToken);

        return product is null ? NotFound() : Ok(product);
    }


    // *Post Product
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            
            
            var created = await _service.CreateAsync(request, cancellationToken);

            if (created is null)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Product code already exists");
            }

            return CreatedAtAction(
                nameof(GetByCode),
                new { productCode = created.ProductCode }, created);
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid product data!",
                detail: e.Message);
        }

    }

    // Update product by id
    [HttpPut("{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Update([FromRoute]string productCode, [FromBody]UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {           

            var updated = await _service.UpdateAsync(productCode, request, cancellationToken);

            return updated is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Produk gagal diubah.", detail: $"Produk dg code {productCode} tidak ditemukan.") : Ok(updated);
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid product data!",
                detail: e.Message);
        }
    }

    [HttpDelete("{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute]string productCode, CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(productCode, cancellationToken);

        return deleted ? NoContent() : Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Product not found",
            detail: $"No product was found for code {productCode}.");
    }
}