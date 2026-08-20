using DarMirFurniture.Localization;
using DarMirFurniture.Models;
using DarMirFurniture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.MyOrders;
        var userId = _userManager.GetUserId(User)!;
        var orders = await _orderService.GetUserOrdersAsync(userId);
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var order = await _orderService.GetOrderDetailAsync(id, userId);
        if (order == null) return NotFound();

        ViewBag.Title = $"الطلب رقم {order.OrderNumber}";
        return View(order);
    }
}