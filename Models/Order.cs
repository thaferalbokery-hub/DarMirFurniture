using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarMirFurniture.Models;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [Display(Name = "رقم الطلب")]
    public string OrderNumber { get; set; } = string.Empty;

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

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "المجموع الفرعي")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "تكلفة الشحن")]
    public decimal ShippingCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "الإجمالي")]
    public decimal Total { get; set; }

    [Display(Name = "الحالة")]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [StringLength(500)]
    [Display(Name = "ملاحظات")]
    public string? Notes { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}