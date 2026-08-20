using DarMirFurniture.Localization;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IReportService _reportService;

    public DashboardController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.AdminDashboard;
        ViewData["PageTitle"] = AppText.Dashboard;
        var dashboard = await _reportService.GetDashboardAsync();
        return View(dashboard);
    }
}