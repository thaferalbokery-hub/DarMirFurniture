using System.ComponentModel.DataAnnotations;
using DarMirFurniture.Models;

namespace DarMirFurniture.ViewModels;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "الاسم الكامل مطلوب")]
    [StringLength(200)]
    [Display(Name = "الاسم الكامل")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "رقم الهاتف مطلوب")]
    [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
    [StringLength(20)]
    [Display(Name = "رقم الهاتف")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "المدينة مطلوبة")]
    [StringLength(100)]
    [Display(Name = "المدينة")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "العنوان مطلوب")]
    [StringLength(500)]
    [Display(Name = "العنوان")]
    public string Address { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "عنوان الشحن")]
    public string? ShippingAddress { get; set; }

    [StringLength(500)]
    [Display(Name = "ملاحظات")]
    public string? Notes { get; set; }

    // Display data
    public List<CartItem> CartItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Total { get; set; }
}