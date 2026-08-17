using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarMirFurniture.Models;

public class Product
{
    [Key]
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
    [Range(0.01, 999999.99, ErrorMessage = "السعر يجب أن يكون بين 0.01 و 999999.99")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Discount Price")]
    public decimal? DiscountPrice { get; set; }

    [StringLength(100)]
    [Display(Name = "Material")]
    public string? Material { get; set; }

    [StringLength(100)]
    [Display(Name = "Color")]
    public string? Color { get; set; }

    // Dimensions
    [Display(Name = "Width (cm)")]
    public double? Width { get; set; }

    [Display(Name = "Height (cm)")]
    public double? Height { get; set; }

    [Display(Name = "Depth (cm)")]
    public double? Depth { get; set; }

    [Display(Name = "Weight (kg)")]
    public double? Weight { get; set; }

    // Stock
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Reorder Level")]
    public int ReorderLevel { get; set; } = 10;

    // Foreign Keys
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [Display(Name = "Brand")]
    public int? BrandId { get; set; }

    // Status flags
    [Display(Name = "Is Available")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Is Featured")]
    public bool IsFeatured { get; set; }

    [Display(Name = "Is New")]
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