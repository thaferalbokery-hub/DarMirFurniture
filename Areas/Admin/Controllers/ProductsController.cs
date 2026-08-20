using DarMirFurniture.Localization;
using DarMirFurniture.Models;
using DarMirFurniture.Services;
using DarMirFurniture.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly IImageService _imageService;

    public ProductsController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IImageService imageService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _imageService = imageService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.ManageProducts;
        ViewData["PageTitle"] = AppText.Products;
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Title = AppText.CreateProduct;
        ViewData["PageTitle"] = AppText.CreateProduct;
        await LoadDropdowns();
        return View(new ProductViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.CreateProduct;
            ViewData["PageTitle"] = AppText.CreateProduct;
            await LoadDropdowns();
            return View(model);
        }

        await _productService.CreateProductAsync(model);
        TempData["Success"] = AppText.ProductCreated;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        ViewBag.Title = AppText.EditProduct;
        ViewData["PageTitle"] = AppText.EditProduct;
        await LoadDropdowns();

        var model = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPrice = product.DiscountPrice,
            Width = product.Width,
            Height = product.Height,
            Depth = product.Depth,
            Weight = product.Weight,
            Material = product.Material,
            Color = product.Color,
            CategoryId = product.CategoryId,
            BrandId = product.BrandId,
            StockQuantity = product.StockQuantity,
            ReorderLevel = product.ReorderLevel,
            IsAvailable = product.IsAvailable,
            IsFeatured = product.IsFeatured,
            IsNew = product.IsNew
        };

        ViewData["ExistingImages"] = product.ProductImages.ToList();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.EditProduct;
            ViewData["PageTitle"] = AppText.EditProduct;
            await LoadDropdowns();

            var existing = await _productService.GetProductByIdAsync(model.Id);
            ViewData["ExistingImages"] = existing?.ProductImages.ToList() ?? new List<ProductImage>();
            return View(model);
        }

        await _productService.UpdateProductAsync(model);
        TempData["Success"] = AppText.ProductUpdated;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["Success"] = AppText.ProductDeleted;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteImage(int imageId, int productId)
    {
        await _productService.DeleteProductImageAsync(imageId, productId);
        TempData["Success"] = AppText.ImageDeleted;
        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    private async Task LoadDropdowns()
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetActiveAsync(), "Id", "Name");
        ViewBag.Brands = new SelectList(await _brandService.GetActiveAsync(), "Id", "Name");
    }
}