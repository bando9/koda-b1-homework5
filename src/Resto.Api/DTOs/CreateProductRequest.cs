using System.ComponentModel.DataAnnotations;

namespace Resto.Api.DTOs;

public record CreateProductRequest(
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId harus lebih besar dari 0.")]
    int CategoryId,

    [Required]
    [StringLength(100, ErrorMessage = "Nama produk maksimal 100 karakter.")]
    string Name,

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Harga tidak boleh negatif.")]
    decimal Price,

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif.")]
    int Stock,

    bool IsActive = true
);