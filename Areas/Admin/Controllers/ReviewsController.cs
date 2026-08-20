using DarMirFurniture.Localization;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.ManageReviews;
        ViewData["PageTitle"] = AppText.Reviews;
        var reviews = await _reviewService.GetAllReviewsAsync();
        return View(reviews);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.DeleteReviewAsync(id, "", isAdmin: true);
        TempData["Success"] = AppText.ReviewDeleted;
        return RedirectToAction(nameof(Index));
    }
}