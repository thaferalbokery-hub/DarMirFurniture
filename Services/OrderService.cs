using DarMirFurniture.Data;
using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;

    public OrderService(ApplicationDbContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public async Task<Order> CreateOrderAsync(string userId, CheckoutViewModel model)
    {
        var cart = await _cartService.GetOrCreateCartAsync(userId);

        if (!cart.CartItems.Any())
            throw new InvalidOperationException("Cart is empty");

        // Validate inventory for all items
        foreach (var item in cart.CartItems)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
            if (product == null || product.StockQuantity < item.Quantity)
                throw new InvalidOperationException($"Insufficient stock for product: {item.Product.Name}");
        }

        var subtotal = cart.CartItems.Sum(ci => ci.Subtotal);
        var shippingCost = subtotal >= 5000 ? 0 : 50m;

        var order = new Order
        {
            UserId = userId,
            OrderNumber = GenerateOrderNumber(),
            FullName = model.FullName,
            Phone = model.Phone,
            City = model.City,
            Address = model.Address,
            ShippingAddress = model.ShippingAddress,
            Notes = model.Notes,
            Subtotal = subtotal,
            ShippingCost = shippingCost,
            Total = subtotal + shippingCost,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Create order items and reduce stock
        foreach (var cartItem in cart.CartItems)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.Name,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice,
                Subtotal = cartItem.Subtotal
            };
            _context.OrderItems.Add(orderItem);

            // Reduce stock
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);
            if (product != null)
            {
                product.StockQuantity -= cartItem.Quantity;
                product.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();

        // Clear cart
        await _cartService.ClearCartAsync(userId);

        return order;
    }

    public async Task<List<Order>> GetUserOrdersAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderDetailAsync(int orderId, string? userId = null)
    {
        var query = _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.ProductImages)
            .Include(o => o.User)
            .AsQueryable();

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(o => o.UserId == userId);

        return await query.FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetTotalOrdersAsync()
    {
        return await _context.Orders.CountAsync();
    }

    public async Task<decimal> GetTotalSalesAsync()
    {
        return await _context.Orders
            .Where(o => o.Status != OrderStatus.Cancelled)
            .SumAsync(o => o.Total);
    }

    private string GenerateOrderNumber()
    {
        return $"DM{DateTime.UtcNow:yyyyMMdd}{new Random().Next(1000, 9999)}";
    }
}