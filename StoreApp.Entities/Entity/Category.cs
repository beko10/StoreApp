namespace StoreApp.Entities.Entity;
public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}

