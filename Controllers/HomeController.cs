using DarMirFurniture.Services;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = "DarMir - Luxury Furniture & Home Decor";
        ViewData["FeaturedProducts"] = await _productService.GetFeaturedProductsAsync(8);
        ViewData["NewProducts"] = await _productService.GetNewProductsAsync(4);
        ViewData["DiscountedProducts"] = await _productService.GetDiscountedProductsAsync(4);
        ViewData["Categories"] = await _categoryService.GetActiveAsync();
        return View();
    }

    public IActionResult About()
    {
        ViewBag.Title = "About DarMir";
        return View();
    }

    public IActionResult Contact()
    {
        ViewBag.Title = "Contact Us";
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}