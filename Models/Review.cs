using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarMirFurniture.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "التقييم مطلوب")]
    [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
    [Display(Name = "التقييم")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "التعليق مطلوب")]
    [StringLength(1000, ErrorMessage = "التعليق يجب أن لا يتجاوز 1000 حرف")]
    [Display(Name = "التعليق")]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
}