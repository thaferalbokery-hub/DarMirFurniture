using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;

namespace DarMirFurniture.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string userId, CheckoutViewModel model);
    Task<List<Order>> GetUserOrdersAsync(string userId);
    Task<Order?> GetOrderDetailAsync(int orderId, string? userId = null);
    Task<List<Order>> GetAllOrdersAsync();
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task<int> GetTotalOrdersAsync();
    Task<decimal> GetTotalSalesAsync();
}