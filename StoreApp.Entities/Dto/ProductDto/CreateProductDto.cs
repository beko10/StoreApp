namespace StoreApp.Entities.Dto;

public class CreateProductDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}