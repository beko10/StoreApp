namespace StoreApp.UI.Areas.Admin.Models;



public class UpdateProductViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? CategoryId { get; set; }
}
