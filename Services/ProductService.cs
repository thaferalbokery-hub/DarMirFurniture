using DarMirFurniture.Data;
using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IImageService _imageService;

    public ProductService(ApplicationDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<ProductFilterViewModel> GetFilteredProductsAsync(ProductFilterViewModel filter)
    {
        var query = _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Reviews)
            .AsQueryable();

        // Row-level filtering with Where()
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(term) ||
                p.Description.ToLower().Contains(term) ||
                (p.Brand != null && p.Brand.Name.ToLower().Contains(term)) ||
                (p.Material != null && p.Material.ToLower().Contains(term)));
        }

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

        if (filter.BrandId.HasValue)
            query = query.Where(p => p.BrandId == filter.BrandId.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        if (filter.IsAvailable.HasValue)
            query = query.Where(p => p.IsAvailable == filter.IsAvailable.Value);

        if (filter.IsFeatured.HasValue)
            query = query.Where(p => p.IsFeatured == filter.IsFeatured.Value);

        if (filter.IsNew.HasValue)
            query = query.Where(p => p.IsNew == filter.IsNew.Value);

        // Sorting
        query = filter.SortBy switch
        {
            "price_asc" => query.OrderBy(p => p.DiscountPrice ?? p.Price),
            "price_desc" => query.OrderByDescending(p => p.DiscountPrice ?? p.Price),
            "name" => query.OrderBy(p => p.Name),
            "newest" => query.OrderByDescending(p => p.CreatedAt),
            "rating" => query.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        filter.TotalCount = await query.CountAsync();

        // Column-level projection with Select()
        filter.Products = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                PrimaryImageUrl = p.ProductImages.FirstOrDefault(i => i.IsPrimary) != null
                    ? p.ProductImages.First(i => i.IsPrimary).ImageUrl
                    : p.ProductImages.FirstOrDefault() != null ? p.ProductImages.First().ImageUrl : null,
                CategoryName = p.Category != null ? p.Category.Name : null,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                IsFeatured = p.IsFeatured,
                IsNew = p.IsNew,
                IsAvailable = p.IsAvailable,
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count
            })
            .ToListAsync();

        // Load filter options
        filter.Categories = await _context.Categories.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
        filter.Brands = await _context.Brands.Where(b => b.IsActive).OrderBy(b => b.Name).ToListAsync();

        return filter;
    }

    public async Task<ProductDetailViewModel?> GetProductDetailAsync(int id, string? userId = null)
    {
        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Reviews).ThenInclude(r => r.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return new ProductDetailViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPrice = product.DiscountPrice,
            Width = product.Width,
            Height = product.Height,
            Depth = product.Depth,
            Weight = product.Weight,
            Material = product.Material,
            Color = product.Color,
            BrandName = product.Brand?.Name,
            CategoryName = product.Category?.Name,
            IsAvailable = product.IsAvailable,
            IsFeatured = product.IsFeatured,
            IsNew = product.IsNew,
            StockQuantity = product.StockQuantity,
            Images = product.ProductImages.OrderBy(i => i.DisplayOrder).ToList(),
            Reviews = product.Reviews.OrderByDescending(r => r.CreatedAt).Select(r => new ReviewViewModel
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                UserName = $"{r.User.FirstName} {r.User.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                IsOwner = r.UserId == userId
            }).ToList(),
            AverageRating = product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = product.Reviews.Count
        };
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ProductListItemViewModel>> GetFeaturedProductsAsync(int count = 8)
    {
        return await _context.Products
            .Where(p => p.IsFeatured && p.IsAvailable)
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Reviews)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                PrimaryImageUrl = p.ProductImages.FirstOrDefault(i => i.IsPrimary) != null
                    ? p.ProductImages.First(i => i.IsPrimary).ImageUrl
                    : p.ProductImages.FirstOrDefault() != null ? p.ProductImages.First().ImageUrl : null,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                IsFeatured = true,
                IsNew = p.IsNew,
                IsAvailable = true,
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count
            })
            .ToListAsync();
    }

    public async Task<List<ProductListItemViewModel>> GetNewProductsAsync(int count = 8)
    {
        return await _context.Products
            .Where(p => p.IsNew && p.IsAvailable)
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Reviews)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                PrimaryImageUrl = p.ProductImages.FirstOrDefault(i => i.IsPrimary) != null
                    ? p.ProductImages.First(i => i.IsPrimary).ImageUrl
                    : p.ProductImages.FirstOrDefault() != null ? p.ProductImages.First().ImageUrl : null,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                IsFeatured = p.IsFeatured,
                IsNew = true,
                IsAvailable = true,
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count
            })
            .ToListAsync();
    }

    public async Task<List<ProductListItemViewModel>> GetDiscountedProductsAsync(int count = 8)
    {
        return await _context.Products
            .Where(p => p.DiscountPrice.HasValue && p.DiscountPrice < p.Price && p.IsAvailable)
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Reviews)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                PrimaryImageUrl = p.ProductImages.FirstOrDefault(i => i.IsPrimary) != null
                    ? p.ProductImages.First(i => i.IsPrimary).ImageUrl
                    : p.ProductImages.FirstOrDefault() != null ? p.ProductImages.First().ImageUrl : null,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                IsFeatured = p.IsFeatured,
                IsNew = p.IsNew,
                IsAvailable = true,
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count
            })
            .ToListAsync();
    }

    public async Task CreateProductAsync(ProductViewModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            DiscountPrice = model.DiscountPrice,
            Width = model.Width,
            Height = model.Height,
            Depth = model.Depth,
            Weight = model.Weight,
            Material = model.Material,
            Color = model.Color,
            CategoryId = model.CategoryId,
            BrandId = model.BrandId,
            StockQuantity = model.StockQuantity,
            ReorderLevel = model.ReorderLevel,
            IsAvailable = model.IsAvailable,
            IsFeatured = model.IsFeatured,
            IsNew = model.IsNew,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Upload images
        if (model.Images != null)
        {
            bool isFirst = true;
            foreach (var image in model.Images)
            {
                if (_imageService.IsValidImage(image))
                {
                    var imageUrl = await _imageService.UploadImageAsync(image);
                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = imageUrl,
                        IsPrimary = isFirst,
                        DisplayOrder = isFirst ? 0 : 1
                    });
                    isFirst = false;
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductViewModel model)
    {
        var product = await _context.Products
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == model.Id);

        if (product == null) return;

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.DiscountPrice = model.DiscountPrice;
        product.Width = model.Width;
        product.Height = model.Height;
        product.Depth = model.Depth;
        product.Weight = model.Weight;
        product.Material = model.Material;
        product.Color = model.Color;
        product.CategoryId = model.CategoryId;
        product.BrandId = model.BrandId;
        product.StockQuantity = model.StockQuantity;
        product.ReorderLevel = model.ReorderLevel;
        product.IsAvailable = model.IsAvailable;
        product.IsFeatured = model.IsFeatured;
        product.IsNew = model.IsNew;
        product.UpdatedAt = DateTime.UtcNow;

        // Upload new images if provided
        if (model.Images != null && model.Images.Any())
        {
            foreach (var image in model.Images)
            {
                if (_imageService.IsValidImage(image))
                {
                    var imageUrl = await _imageService.UploadImageAsync(image);
                    _context.ProductImages.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = imageUrl,
                        IsPrimary = false,
                        DisplayOrder = 99
                    });
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return;

        // Delete physical image files
        foreach (var image in product.ProductImages)
        {
            await _imageService.DeleteImageAsync(image.ImageUrl);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Products.CountAsync();
    }
}