using DarMirFurniture.Models;

namespace DarMirFurniture.ViewModels;

public class ProductDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Depth { get; set; }
    public double? Weight { get; set; }
    public string? Material { get; set; }
    public string? Color { get; set; }
    public string? BrandName { get; set; }
    public string? CategoryName { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public int StockQuantity { get; set; }

    public List<ProductImage> Images { get; set; } = new();
    public List<ReviewViewModel> Reviews { get; set; } = new();
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}