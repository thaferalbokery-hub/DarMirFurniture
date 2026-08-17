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
        ViewBag.Title = "Manage Brands";
        ViewData["PageTitle"] = "Brands";
        var brands = await _brandService.GetAllAsync();
        return View(brands);
    }

    public IActionResult Create()
    {
        ViewBag.Title = "Create Brand";
        ViewData["PageTitle"] = "Create Brand";
        return View(new Brand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand brand, IFormFile? logo)
    {
        if (!ModelState.IsValid) return View(brand);

        if (logo != null && _imageService.IsValidImage(logo))
        {
            brand.LogoUrl = await _imageService.UploadImageAsync(logo, "brands");
        }

        await _brandService.CreateAsync(brand);
        TempData["Success"] = "تم إنشاء العلامة التجارية بنجاح";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        if (brand == null) return NotFound();

        ViewBag.Title = "Edit Brand";
        ViewData["PageTitle"] = "Edit Brand";
        return View(brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Brand brand, IFormFile? logo)
    {
        if (!ModelState.IsValid) return View(brand);

        if (logo != null && _imageService.IsValidImage(logo))
        {
            if (!string.IsNullOrEmpty(brand.LogoUrl))
                await _imageService.DeleteImageAsync(brand.LogoUrl);
            brand.LogoUrl = await _imageService.UploadImageAsync(logo, "brands");
        }

        await _brandService.UpdateAsync(brand);
        TempData["Success"] = "تم تحديث العلامة التجارية بنجاح";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _brandService.DeleteAsync(id);
        TempData["Success"] = "تم حذف العلامة التجارية بنجاح";
        return RedirectToAction(nameof(Index));
    }
}