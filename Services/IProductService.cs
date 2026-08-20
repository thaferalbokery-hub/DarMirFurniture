using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;

namespace DarMirFurniture.Services;

public interface IProductService
{
    Task<ProductFilterViewModel> GetFilteredProductsAsync(ProductFilterViewModel filter);
    Task<ProductDetailViewModel?> GetProductDetailAsync(int id, string? userId = null);
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<ProductListItemViewModel>> GetFeaturedProductsAsync(int count = 8);
    Task<List<ProductListItemViewModel>> GetNewProductsAsync(int count = 8);
    Task<List<ProductListItemViewModel>> GetDiscountedProductsAsync(int count = 8);
    Task CreateProductAsync(ProductViewModel model);
    Task UpdateProductAsync(ProductViewModel model);
    Task DeleteProductAsync(int id);
    Task DeleteProductImageAsync(int imageId, int productId);
    Task<int> GetTotalCountAsync();
}