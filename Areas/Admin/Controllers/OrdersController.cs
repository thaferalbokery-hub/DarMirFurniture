using DarMirFurniture.Models;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = "Manage Orders";
        ViewData["PageTitle"] = "Orders";
        var orders = await _orderService.GetAllOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailAsync(id);
        if (order == null) return NotFound();

        ViewBag.Title = $"Order #{order.OrderNumber}";
        ViewData["PageTitle"] = $"Order #{order.OrderNumber}";
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus status)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, status);
        TempData["Success"] = "تم تحديث حالة الطلب بنجاح";
        return RedirectToAction(nameof(Details), new { id = orderId });
    }
}