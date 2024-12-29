namespace StoreApp.Entities.Dto;
public class UpdateProductDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public string? CategoryName { get; set; }
}
