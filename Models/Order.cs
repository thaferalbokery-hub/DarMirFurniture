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
    [Display(Name = "Order Number")]
    public string OrderNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20)]
    [Display(Name = "Phone")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "City")]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    [Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Shipping Address")]
    public string? ShippingAddress { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Subtotal")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Shipping Cost")]
    public decimal ShippingCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total")]
    public decimal Total { get; set; }

    [Display(Name = "Status")]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [StringLength(500)]
    [Display(Name = "Notes")]
    public string? Notes { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}