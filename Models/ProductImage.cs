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
    public string ImageUrl { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Alt Text")]
    public string? AltText { get; set; }

    [Display(Name = "Is Primary")]
    public bool IsPrimary { get; set; }

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property
    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;
}