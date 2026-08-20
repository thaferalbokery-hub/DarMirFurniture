using DarMirFurniture.Localization;
using DarMirFurniture.Models;
using DarMirFurniture.Services;
using DarMirFurniture.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarMirFurniture.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _orderService = orderService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Title = AppText.Cart;
        var userId = _userManager.GetUserId(User)!;
        var cart = await _cartService.GetOrCreateCartAsync(userId);
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        var userId = _userManager.GetUserId(User)!;
        await _cartService.AddToCartAsync(userId, productId, quantity);
        TempData["Success"] = AppText.ProductAddedToCart;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
    {
        var userId = _userManager.GetUserId(User)!;
        await _cartService.UpdateQuantityAsync(userId, cartItemId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int cartItemId)
    {
        var userId = _userManager.GetUserId(User)!;
        await _cartService.RemoveFromCartAsync(userId, cartItemId);
        TempData["Success"] = AppText.ProductRemovedFromCart;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Checkout()
    {
        ViewBag.Title = AppText.Checkout;
        var userId = _userManager.GetUserId(User)!;
        var user = await _userManager.GetUserAsync(User);
        var cart = await _cartService.GetOrCreateCartAsync(userId);

        if (!cart.CartItems.Any())
        {
            TempData["Error"] = AppText.EmptyCartError;
            return RedirectToAction(nameof(Index));
        }

        var subtotal = cart.CartItems.Sum(ci => ci.Subtotal);
        var shippingCost = CurrencySettings.CalculateShipping(subtotal);

        var model = new CheckoutViewModel
        {
            FullName = $"{user!.FirstName} {user.LastName}",
            Phone = user.Phone ?? "",
            City = user.City ?? "",
            Address = user.Address ?? "",
            ShippingAddress = user.ShippingAddress,
            CartItems = cart.CartItems.ToList(),
            Subtotal = subtotal,
            ShippingCost = shippingCost,
            Total = subtotal + shippingCost
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
    {
        var userId = _userManager.GetUserId(User)!;

        if (!ModelState.IsValid)
        {
            ViewBag.Title = AppText.Checkout;
            var currentCart = await _cartService.GetOrCreateCartAsync(userId);
            var currentSubtotal = currentCart.CartItems.Sum(ci => ci.Subtotal);
            model.CartItems = currentCart.CartItems.ToList();
            model.Subtotal = currentSubtotal;
            model.ShippingCost = CurrencySettings.CalculateShipping(currentSubtotal);
            model.Total = currentSubtotal + model.ShippingCost;
            return View("Checkout", model);
        }

        try
        {
            var order = await _orderService.CreateOrderAsync(userId, model);
            TempData["Success"] = AppText.OrderCreated;
            return RedirectToAction(nameof(OrderConfirmation), new { id = order.Id });
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Checkout));
        }
    }

    public async Task<IActionResult> OrderConfirmation(int id)
    {
        ViewBag.Title = AppText.OrderConfirmationTitle;
        var userId = _userManager.GetUserId(User)!;
        var order = await _orderService.GetOrderDetailAsync(id, userId);
        if (order == null) return NotFound();
        return View(order);
    }
}