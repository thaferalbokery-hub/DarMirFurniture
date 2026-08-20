using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.Models;

public class Brand
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم العلامة التجارية مطلوب")]
    [StringLength(100)]
    [Display(Name = "اسم العلامة التجارية")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "الوصف")]
    public string? Description { get; set; }

    [Display(Name = "الشعار")]
    public string? LogoUrl { get; set; }

    [Url(ErrorMessage = "رابط الموقع غير صالح")]
    [StringLength(200)]
    [Display(Name = "الموقع الإلكتروني")]
    public string? Website { get; set; }

    [Display(Name = "مفعّل")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}