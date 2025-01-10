using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using StoreApp.Business.Abstract;
using StoreApp.DataAccess.UnitOfWork;
using StoreApp.Entities.Entity;

namespace StoreApp.UI.Pages;


public class CartModel : PageModel
{
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; } = "/";

    private readonly IProductService _productService;
    private readonly IMapper _mapper;


    public CartModel(IProductService productService, IMapper mapper, Cart cart)
    {
        _productService = productService;
        _mapper = mapper;
        Cart = cart;
    }

    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public async Task<IActionResult> OnPost(string id,string returnUrl)
    {
        var result = await _productService.GetByIdAsync(id,false);
        var addCartItem= _mapper.Map<Product>(result.Data);
        if(result.IsSuccess)
        {
            Cart?.AddItem(addCartItem,1);
        }
        return Page();
    }

    public async Task<IActionResult> OnPostRemove(string id,string returnUrl)
    {
        var result = await _productService.GetByIdAsync(id,false);
        var removeCartItem = _mapper.Map<Product>(result.Data);
        if(result.IsSuccess)
        {
            Cart?.RemoveCartItemLine(Cart.CartItems.First(cl => cl.Product.Id == id).Product);
        }
        return Page();
    }
}