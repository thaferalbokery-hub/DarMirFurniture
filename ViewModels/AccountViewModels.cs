using System.ComponentModel.DataAnnotations;

namespace DarMirFurniture.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "تذكرني")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "الاسم الأول مطلوب")]
    [StringLength(100)]
    [Display(Name = "الاسم الأول")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "اسم العائلة مطلوب")]
    [StringLength(100)]
    [Display(Name = "اسم العائلة")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "كلمات المرور غير متطابقة")]
    [Display(Name = "تأكيد كلمة المرور")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class ProfileViewModel
{
    [Required(ErrorMessage = "الاسم الأول مطلوب")]
    [StringLength(100)]
    [Display(Name = "الاسم الأول")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "اسم العائلة مطلوب")]
    [StringLength(100)]
    [Display(Name = "اسم العائلة")]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
    [StringLength(20)]
    [Display(Name = "رقم الهاتف")]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    [Display(Name = "المدينة")]
    public string? City { get; set; }

    [StringLength(500)]
    [Display(Name = "العنوان")]
    public string? Address { get; set; }

    [StringLength(500)]
    [Display(Name = "عنوان الشحن")]
    public string? ShippingAddress { get; set; }
}