using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.ViewModels;

public class ReviewViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "التقييم مطلوب")]
    [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
    [Display(Name = "التقييم")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "التعليق مطلوب")]
    [StringLength(1000, ErrorMessage = "التعليق يجب أن لا يتجاوز 1000 حرف")]
    [Display(Name = "التعليق")]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public bool IsOwner { get; set; }
}

public class CreateReviewViewModel
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "التقييم مطلوب")]
    [Range(1, 5, ErrorMessage = "التقييم يجب أن يكون بين 1 و 5")]
    [Display(Name = "التقييم")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "التعليق مطلوب")]
    [StringLength(1000, ErrorMessage = "التعليق يجب أن لا يتجاوز 1000 حرف")]
    [Display(Name = "التعليق")]
    public string Comment { get; set; } = string.Empty;
}