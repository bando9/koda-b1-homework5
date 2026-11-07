using Microsoft.AspNetCore.Mvc;
using Resto.Api.DTOs;
using Resto.Api.Services;
using RestoApi.DTOs;

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


    // Get product by Id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById([FromRoute]int id, CancellationToken cancellationToken)
    {
        var product = await _service.GetByIdAsync(id, cancellationToken);

        return product is null ? NotFound() : Ok(product);
    }


    // Post Product
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Create([FromBody]CreateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            
            
            var created = await _service.CreateAsync(request, cancellationToken);

            if (created is null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Produk gagal dibuat.",
                    Detail = "Pastikan CategoryId valid serta Price dan Stock tidak negatif."
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Update([FromRoute]int id, [FromBody]UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {           

            var updated = await _service.UpdateAsync(id, request, cancellationToken);

            return updated is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Produk gagal diubah.", detail: $"Produk tidak ditemukan, atau CategoryId/Price/Stock tidak valid.") : Ok(updated);
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid product data!",
                detail: e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Produk gagal dihapus.",
                Detail = "Produk tidak ditemukan, atau masih dipakai di cart. Nonaktifkan dengan IsActive = false."
            });
        }

        return NoContent();
    }
}