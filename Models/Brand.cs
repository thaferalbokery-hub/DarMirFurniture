using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.Models;

public class Brand
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم العلامة التجارية مطلوب")]
    [StringLength(100)]
    [Display(Name = "Brand Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Logo")]
    public string? LogoUrl { get; set; }

    [Url]
    [StringLength(200)]
    [Display(Name = "Website")]
    public string? Website { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}