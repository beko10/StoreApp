using Microsoft.AspNetCore.Mvc;
using StoreApp.Business.Abstract;

namespace StoreApp.UI.Components;

public class CategoryListViewComponent :ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoryListViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _categoryService.GetAllAsync();
        if(result.IsSuccess)
        {
            return View(result.Data);
        }
        return View(result.Message);
    }
}