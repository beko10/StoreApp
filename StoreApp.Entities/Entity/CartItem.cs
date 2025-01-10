namespace StoreApp.Entities.Entity;

public class CartItem:BaseEntity
{
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}