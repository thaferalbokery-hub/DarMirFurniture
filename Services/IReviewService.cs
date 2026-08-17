using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;

namespace DarMirFurniture.Services;

public interface IReviewService
{
    Task<List<ReviewViewModel>> GetProductReviewsAsync(int productId, string? currentUserId = null);
    Task<List<Review>> GetAllReviewsAsync();
    Task CreateReviewAsync(string userId, CreateReviewViewModel model);
    Task UpdateReviewAsync(int reviewId, string userId, CreateReviewViewModel model);
    Task DeleteReviewAsync(int reviewId, string userId, bool isAdmin = false);
    Task<Review?> GetByIdAsync(int id);
}