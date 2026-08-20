using DarMirFurniture.Localization;
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
        ViewBag.Title = AppText.ManageOrders;
        ViewData["PageTitle"] = AppText.Orders;
        var orders = await _orderService.GetAllOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailAsync(id);
        if (order == null) return NotFound();

        ViewBag.Title = $"الطلب رقم {order.OrderNumber}";
        ViewData["PageTitle"] = $"الطلب رقم {order.OrderNumber}";
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus status)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, status);
        TempData["Success"] = AppText.OrderStatusUpdated;
        return RedirectToAction(nameof(Details), new { id = orderId });
    }
}