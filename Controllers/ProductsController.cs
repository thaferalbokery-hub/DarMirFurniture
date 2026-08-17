using DarMirFurniture.Services;
using DarMirFurniture.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DarMirFurniture.Models;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IReviewService _reviewService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductsController(IProductService productService, IReviewService reviewService, UserManager<ApplicationUser> userManager)
    {
        _productService = productService;
        _reviewService = reviewService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(ProductFilterViewModel filter)
    {
        ViewBag.Title = "Our Products";
        var result = await _productService.GetFilteredProductsAsync(filter);
        return View(result);
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager.GetUserId(User);
        var product = await _productService.GetProductDetailAsync(id, userId);
        if (product == null) return NotFound();

        ViewBag.Title = product.Name;
        ViewData["ProductName"] = product.Name;
        return View(product);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(CreateReviewViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Details), new { id = model.ProductId });
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _reviewService.CreateReviewAsync(userId, model);
        TempData["Success"] = "تم إضافة المراجعة بنجاح";
        return RedirectToAction(nameof(Details), new { id = model.ProductId });
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(int reviewId, int productId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        await _reviewService.DeleteReviewAsync(reviewId, userId);
        TempData["Success"] = "تم حذف المراجعة";
        return RedirectToAction(nameof(Details), new { id = productId });
    }
}