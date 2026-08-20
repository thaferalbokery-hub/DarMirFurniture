using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarMirFurniture.Models;

public class Product
{
    [Key]
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
    [Range(0.01, 99999999.99, ErrorMessage = "السعر يجب أن يكون بين 0.01 و 99,999,999.99 ريال يمني")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "السعر (ر.ي)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "سعر الخصم (ر.ي)")]
    public decimal? DiscountPrice { get; set; }

    [StringLength(100)]
    [Display(Name = "المادة")]
    public string? Material { get; set; }

    [StringLength(100)]
    [Display(Name = "اللون")]
    public string? Color { get; set; }

    // Dimensions
    [Display(Name = "العرض (سم)")]
    public double? Width { get; set; }

    [Display(Name = "الارتفاع (سم)")]
    public double? Height { get; set; }

    [Display(Name = "العمق (سم)")]
    public double? Depth { get; set; }

    [Display(Name = "الوزن (كجم)")]
    public double? Weight { get; set; }

    // Stock
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "الكمية بالمخزون")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "حد إعادة الطلب")]
    public int ReorderLevel { get; set; } = 10;

    // Foreign Keys
    [Display(Name = "الفئة")]
    public int? CategoryId { get; set; }

    [Display(Name = "العلامة التجارية")]
    public int? BrandId { get; set; }

    // Status flags
    [Display(Name = "متوفر")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "منتج مميز")]
    public bool IsFeatured { get; set; }

    [Display(Name = "منتج جديد")]
    public bool IsNew { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}