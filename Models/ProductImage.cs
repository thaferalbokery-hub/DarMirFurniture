using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarMirFurniture.Models;

public class ProductImage
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "رابط الصورة")]
    public string ImageUrl { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "النص البديل")]
    public string? AltText { get; set; }

    [Display(Name = "الصورة الرئيسية")]
    public bool IsPrimary { get; set; }

    [Display(Name = "ترتيب العرض")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property
    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}