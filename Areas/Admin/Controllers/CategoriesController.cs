using DarMirFurniture.Localization;
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
        ViewBag.Title = AppText.ManageCategories;
        ViewData["PageTitle"] = AppText.Categories;
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        ViewBag.Title = AppText.CreateCategory;
        ViewData["PageTitle"] = AppText.CreateCategory;
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.CreateCategory;
            ViewData["PageTitle"] = AppText.CreateCategory;
            return View(category);
        }

        if (image != null && _imageService.IsValidImage(image))
        {
            category.ImageUrl = await _imageService.UploadImageAsync(image, "categories");
        }

        await _categoryService.CreateAsync(category);
        TempData["Success"] = AppText.CategoryCreated;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        ViewBag.Title = AppText.EditCategory;
        ViewData["PageTitle"] = AppText.EditCategory;
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.EditCategory;
            ViewData["PageTitle"] = AppText.EditCategory;
            return View(category);
        }

        if (image != null && _imageService.IsValidImage(image))
        {
            if (!string.IsNullOrEmpty(category.ImageUrl))
                await _imageService.DeleteImageAsync(category.ImageUrl);
            category.ImageUrl = await _imageService.UploadImageAsync(image, "categories");
        }

        await _categoryService.UpdateAsync(category);
        TempData["Success"] = AppText.CategoryUpdated;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        TempData["Success"] = AppText.CategoryDeleted;
        return RedirectToAction(nameof(Index));
    }
}