using DarMirFurniture.Data;
using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewViewModel>> GetProductReviewsAsync(int productId, string? currentUserId = null)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewViewModel
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                UserName = r.User.FirstName + " " + r.User.LastName,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                IsOwner = r.UserId == currentUserId
            })
            .ToListAsync();
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task CreateReviewAsync(string userId, CreateReviewViewModel model)
    {
        var review = new Review
        {
            ProductId = model.ProductId,
            UserId = userId,
            Rating = model.Rating,
            Comment = model.Comment,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(int reviewId, string userId, CreateReviewViewModel model)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);
        if (review == null) return;

        review.Rating = model.Rating;
        review.Comment = model.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int reviewId, string userId, bool isAdmin = false)
    {
        var review = isAdmin
            ? await _context.Reviews.FindAsync(reviewId)
            : await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews.Include(r => r.User).Include(r => r.Product).FirstOrDefaultAsync(r => r.Id == id);
    }
}