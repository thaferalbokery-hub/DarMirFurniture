using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم الفئة مطلوب")]
    [StringLength(100, ErrorMessage = "اسم الفئة يجب أن لا يتجاوز 100 حرف")]
    [Display(Name = "Category Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Image")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties - One-to-Many
    public ICollection<Product> Products { get; set; } = new List<Product>();
}