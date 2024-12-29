using Microsoft.AspNetCore.Mvc;
using StoreApp.Business.Abstract;

namespace StoreApp.UI.Components;

public class ProductTotalCountViewComponent:ViewComponent
{
    private readonly IProductService _productService;

    public ProductTotalCountViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<string> InvokeAsync()
    {
       var result =  await _productService.GetProductCountAsync();
       return result.Data.ToString(); 
    }
}