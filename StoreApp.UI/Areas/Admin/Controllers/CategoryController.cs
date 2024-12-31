using Microsoft.AspNetCore.Mvc;

namespace StoreApp.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
}