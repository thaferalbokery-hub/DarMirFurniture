using DarMirFurniture.Localization;
using DarMirFurniture.Models;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IImageService _imageService;

    public BrandsController(IBrandService brandService, IImageService imageService)
    {
        _brandService = brandService;
        _imageService = imageService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.ManageBrands;
        ViewData["PageTitle"] = AppText.Brands;
        var brands = await _brandService.GetAllAsync();
        return View(brands);
    }

    public IActionResult Create()
    {
        ViewBag.Title = AppText.CreateBrand;
        ViewData["PageTitle"] = AppText.CreateBrand;
        return View(new Brand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand brand, IFormFile? logo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.CreateBrand;
            ViewData["PageTitle"] = AppText.CreateBrand;
            return View(brand);
        }

        if (logo != null && _imageService.IsValidImage(logo))
        {
            brand.LogoUrl = await _imageService.UploadImageAsync(logo, "brands");
        }

        await _brandService.CreateAsync(brand);
        TempData["Success"] = AppText.BrandCreated;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        if (brand == null) return NotFound();

        ViewBag.Title = AppText.EditBrand;
        ViewData["PageTitle"] = AppText.EditBrand;
        return View(brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Brand brand, IFormFile? logo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.EditBrand;
            ViewData["PageTitle"] = AppText.EditBrand;
            return View(brand);
        }

        if (logo != null && _imageService.IsValidImage(logo))
        {
            if (!string.IsNullOrEmpty(brand.LogoUrl))
                await _imageService.DeleteImageAsync(brand.LogoUrl);
            brand.LogoUrl = await _imageService.UploadImageAsync(logo, "brands");
        }

        await _brandService.UpdateAsync(brand);
        TempData["Success"] = AppText.BrandUpdated;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _brandService.DeleteAsync(id);
        TempData["Success"] = AppText.BrandDeleted;
        return RedirectToAction(nameof(Index));
    }
}