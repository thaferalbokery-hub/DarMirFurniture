using DarMirFurniture.Localization;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ReportsController : Controller
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Sales(DateTime? startDate, DateTime? endDate)
    {
        ViewBag.Title = AppText.SalesReport;
        ViewData["PageTitle"] = AppText.SalesReport;
        var report = await _reportService.GetSalesReportAsync(startDate, endDate);
        return View(report);
    }

    public async Task<IActionResult> Products()
    {
        ViewBag.Title = AppText.ProductsReport;
        ViewData["PageTitle"] = AppText.ProductsReport;
        var report = await _reportService.GetProductReportAsync();
        return View(report);
    }

    public async Task<IActionResult> Customers()
    {
        ViewBag.Title = AppText.CustomersReport;
        ViewData["PageTitle"] = AppText.CustomersReport;
        var report = await _reportService.GetCustomerReportAsync();
        return View(report);
    }
}