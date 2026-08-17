using DarMirFurniture.Models;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IImageService _imageService;

    public CategoriesController(ICategoryService categoryService, IImageService imageService)
    {
        _categoryService = categoryService;
        _imageService = imageService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = "Manage Categories";
        ViewData["PageTitle"] = "Categories";
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        ViewBag.Title = "Create Category";
        ViewData["PageTitle"] = "Create Category";
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category, IFormFile? image)
    {
        if (!ModelState.IsValid) return View(category);

        if (image != null && _imageService.IsValidImage(image))
        {
            category.ImageUrl = await _imageService.UploadImageAsync(image, "categories");
        }

        await _categoryService.CreateAsync(category);
        TempData["Success"] = "تم إنشاء الفئة بنجاح";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        ViewBag.Title = "Edit Category";
        ViewData["PageTitle"] = "Edit Category";
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category, IFormFile? image)
    {
        if (!ModelState.IsValid) return View(category);

        if (image != null && _imageService.IsValidImage(image))
        {
            if (!string.IsNullOrEmpty(category.ImageUrl))
                await _imageService.DeleteImageAsync(category.ImageUrl);
            category.ImageUrl = await _imageService.UploadImageAsync(image, "categories");
        }

        await _categoryService.UpdateAsync(category);
        TempData["Success"] = "تم تحديث الفئة بنجاح";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        TempData["Success"] = "تم حذف الفئة بنجاح";
        return RedirectToAction(nameof(Index));
    }
}