namespace StoreApp.Entities.Entity;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryId { get; set; }
    public Category? Category { get; set; }
}
