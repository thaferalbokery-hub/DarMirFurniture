using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DarMirFurniture.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم المنتج مطلوب")]
    [StringLength(200)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "وصف المنتج مطلوب")]
    [StringLength(2000)]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "السعر مطلوب")]
    [Range(0.01, 999999.99)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Display(Name = "Discount Price")]
    public decimal? DiscountPrice { get; set; }

    [StringLength(100)]
    [Display(Name = "Material")]
    public string? Material { get; set; }

    [StringLength(100)]
    [Display(Name = "Color")]
    public string? Color { get; set; }

    [Display(Name = "Width (cm)")]
    public double? Width { get; set; }

    [Display(Name = "Height (cm)")]
    public double? Height { get; set; }

    [Display(Name = "Depth (cm)")]
    public double? Depth { get; set; }

    [Display(Name = "Weight (kg)")]
    public double? Weight { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [Display(Name = "Brand")]
    public int? BrandId { get; set; }

    [Display(Name = "Stock Quantity")]
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Display(Name = "Reorder Level")]
    [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; } = 10;

    [Display(Name = "Is Available")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Is Featured")]
    public bool IsFeatured { get; set; }

    [Display(Name = "Is New")]
    public bool IsNew { get; set; } = true;

    [Display(Name = "Product Images")]
    public List<IFormFile>? Images { get; set; }
}