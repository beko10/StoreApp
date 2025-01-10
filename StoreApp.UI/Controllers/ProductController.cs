using Microsoft.AspNetCore.Mvc;
using StoreApp.Business.Abstract;

namespace StoreApp.UI.Controllers;

public class ProductController:Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _productService.GetAllAsync();
        if(result.IsSuccess)
        {
            return View(result.Data);
        }
        return View(result.Message);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var result = await _productService.GetByIdAsync(id);

        if(result.IsSuccess)
        {
            return View(result.Data);
        }
        return View(result.Message);
    }
}