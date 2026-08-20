using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DarMirFurniture.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم المنتج مطلوب")]
    [StringLength(200)]
    [Display(Name = "اسم المنتج")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "وصف المنتج مطلوب")]
    [StringLength(2000)]
    [Display(Name = "الوصف")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "السعر مطلوب")]
    [Range(0.01, 99999999.99, ErrorMessage = "السعر يجب أن يكون رقماً صحيحاً بالريال اليمني")]
    [Display(Name = "السعر (ر.ي)")]
    public decimal Price { get; set; }

    [Display(Name = "سعر الخصم (ر.ي)")]
    public decimal? DiscountPrice { get; set; }

    [StringLength(100)]
    [Display(Name = "المادة")]
    public string? Material { get; set; }

    [StringLength(100)]
    [Display(Name = "اللون")]
    public string? Color { get; set; }

    [Display(Name = "العرض (سم)")]
    public double? Width { get; set; }

    [Display(Name = "الارتفاع (سم)")]
    public double? Height { get; set; }

    [Display(Name = "العمق (سم)")]
    public double? Depth { get; set; }

    [Display(Name = "الوزن (كجم)")]
    public double? Weight { get; set; }

    [Display(Name = "الفئة")]
    public int? CategoryId { get; set; }

    [Display(Name = "العلامة التجارية")]
    public int? BrandId { get; set; }

    [Display(Name = "الكمية بالمخزون")]
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Display(Name = "حد إعادة الطلب")]
    [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; } = 10;

    [Display(Name = "متوفر")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "منتج مميز")]
    public bool IsFeatured { get; set; }

    [Display(Name = "منتج جديد")]
    public bool IsNew { get; set; } = true;

    [Display(Name = "صور المنتج")]
    public List<IFormFile>? Images { get; set; }
}