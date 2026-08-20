using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم الفئة مطلوب")]
    [StringLength(100, ErrorMessage = "اسم الفئة يجب أن لا يتجاوز 100 حرف")]
    [Display(Name = "اسم الفئة")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "الوصف")]
    public string? Description { get; set; }

    [Display(Name = "الصورة")]
    public string? ImageUrl { get; set; }

    [Display(Name = "مفعّل")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "ترتيب العرض")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties - One-to-Many
    public ICollection<Product> Products { get; set; } = new List<Product>();
}