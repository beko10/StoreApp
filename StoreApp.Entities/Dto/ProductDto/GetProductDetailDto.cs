namespace StoreApp.Entities.Dto;

public class GetProductDetailDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}