using DarMirFurniture.Data;
using DarMirFurniture.Models;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> GetOrCreateCartAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                    .ThenInclude(p => p.ProductImages)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddToCartAsync(string userId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(userId);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null || !product.IsAvailable) return;

        var availableStock = product.StockQuantity;

        // Check if item already in cart
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem != null)
        {
            var newQuantity = existingItem.Quantity + quantity;
            if (newQuantity > availableStock) newQuantity = availableStock;
            existingItem.Quantity = newQuantity;
        }
        else
        {
            if (quantity > availableStock) quantity = availableStock;

            decimal unitPrice = product.DiscountPrice ?? product.Price;

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };
            _context.CartItems.Add(cartItem);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuantityAsync(string userId, int cartItemId, int quantity)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return;

        var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (item == null) return;

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
        var availableStock = product?.StockQuantity ?? 0;

        if (quantity <= 0)
        {
            _context.CartItems.Remove(item);
        }
        else
        {
            item.Quantity = Math.Min(quantity, availableStock);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(string userId, int cartItemId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return;

        var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetCartItemCountAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart?.CartItems.Sum(ci => ci.Quantity) ?? 0;
    }
}