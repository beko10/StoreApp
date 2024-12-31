using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApp.Business.Abstract;
using StoreApp.Entities.Dto;

namespace StoreApp.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _productService.GetAllAsync(false);
        if (result.IsSuccess)
        {
            return View(result.Data);
        }
        return View(result.Message);
    }

    public async Task<IActionResult> Create()
    {
        var result = await _categoryService.GetAllAsync(false);
        if (!result.IsSuccess)
        {
            return View(result.Message);
        }
        ViewBag.CategorySelectList = new SelectList(result.Data, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductDto createProductDto)
    {
        if (!ModelState.IsValid)
        {
            var selectedListCategoryResult = await _categoryService.GetAllAsync(false);
            if (!selectedListCategoryResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, selectedListCategoryResult.Message ?? "Bir hata oluştu");
                return View(selectedListCategoryResult.Message);
            }
            ViewBag.CategorySelectList = new SelectList(selectedListCategoryResult.Data, "Id", "Name");
            return View(createProductDto);
        }
        var result = await _productService.AddAsync(createProductDto);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View();
    }

    public async Task<IActionResult> Update(string id)
    {
        // Ürün bilgilerini al
        var productResult = await _productService.GetByIdAsync(id, false);
        var updateProductDto = _mapper.Map<UpdateProductDto>(productResult.Data);
        if (!productResult.IsSuccess || productResult.Data == null)
        {
            // Ürün bulunamadığında özel bir hata sayfasına yönlendirme yapılabilir
            ModelState.AddModelError(string.Empty, productResult.Message ?? "Ürün bulunamadı.");
            return View(); // Ya da hata sayfasına yönlendirme yapılabilir
        }

        // Kategori listesini al
        var categoryResult = await _categoryService.GetAllAsync(false);
        if (!categoryResult.IsSuccess)
        {
            // Hata durumunda kullanıcıya mesaj göster
            ModelState.AddModelError(string.Empty, categoryResult.Message ?? "Kategoriler yüklenirken bir hata oluştu.");
            return View(productResult.Data);
        }

        // ViewBag'e kategori listesini ekle
        ViewBag.CategorySelectList = new SelectList(categoryResult.Data, "Id", "Name", productResult.Data.CategoryId);

        return View(updateProductDto); // Mevcut ürün bilgilerini View'a gönder
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
    {
        if (!ModelState.IsValid)
        {
            // Kategorileri tekrar yükle
            var categoryResult = await _categoryService.GetAllAsync(false);
            if (!categoryResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, categoryResult.Message ?? "Kategoriler yüklenirken bir hata oluştu.");
                return View(updateProductDto);
            }

            ViewBag.CategorySelectList = new SelectList(categoryResult.Data, "Id", "Name");
            return View(updateProductDto); // Formu tekrar göster
        }

        // Güncelleme işlemini yap
        var result = await _productService.UpdateAsync(updateProductDto);
        if (result.IsSuccess)
        {
            // Başarılı güncelleme sonrası liste sayfasına yönlendirme
            return RedirectToAction("Index");
        }

        // Hata durumunda kullanıcıya mesaj göster
        ModelState.AddModelError(string.Empty, result.Message ?? "Ürün güncellenirken bir hata oluştu.");
        var categoriesRetry = await _categoryService.GetAllAsync(false);
        ViewBag.CategorySelectList = new SelectList(categoriesRetry.Data, "Id", "Name");
        return View(updateProductDto);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var result = await _productService.DeleteAsync(id);
        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View(result.Message);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var result = await _productService.GetProductDetailByIdAsync(id,false);
        if (result.IsSuccess)
        {
            return View(result.Data);
        }
        return View(result.Message);
    }


}