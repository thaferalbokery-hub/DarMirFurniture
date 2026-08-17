using DarMirFurniture.Models;

namespace DarMirFurniture.Services;

public interface ICartService
{
    Task<Cart> GetOrCreateCartAsync(string userId);
    Task AddToCartAsync(string userId, int productId, int quantity);
    Task UpdateQuantityAsync(string userId, int cartItemId, int quantity);
    Task RemoveFromCartAsync(string userId, int cartItemId);
    Task ClearCartAsync(string userId);
    Task<int> GetCartItemCountAsync(string userId);
}