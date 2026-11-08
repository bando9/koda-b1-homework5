namespace Resto.Api.Models;

public class Product
{
    public int CategoryId { get; set; }
    public string ProductCode { get; }

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Product(int categoryId,string productCode, string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(productCode))
        {
            throw new ArgumentException("Product code is Required", nameof(productCode));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product code is Required", nameof(name));
        }
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative");
        }
        if (stock < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stock), "stock cannot be negative");
        }
        if (categoryId <0)
        {
            throw new ArgumentOutOfRangeException(nameof(stock), "categoryId cannot be negative");
         }
        ProductCode = productCode;
        Name = name;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }
}