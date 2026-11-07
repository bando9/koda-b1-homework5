namespace Resto.Api.Models;

public class Cart
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}