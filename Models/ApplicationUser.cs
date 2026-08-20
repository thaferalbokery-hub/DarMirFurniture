using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.Models;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "الاسم الأول مطلوب")]
    [StringLength(100)]
    [Display(Name = "الاسم الأول")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "اسم العائلة مطلوب")]
    [StringLength(100)]
    [Display(Name = "اسم العائلة")]
    public string LastName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
    [StringLength(20)]
    [Display(Name = "رقم الهاتف")]
    public string? Phone { get; set; }

    [StringLength(100)]
    [Display(Name = "المدينة")]
    public string? City { get; set; }

    [StringLength(500)]
    [Display(Name = "العنوان")]
    public string? Address { get; set; }

    [StringLength(500)]
    [Display(Name = "عنوان الشحن")]
    public string? ShippingAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Cart? Cart { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}