using Microsoft.AspNetCore.Identity;

namespace DarMirFurniture.Localization;

/// <summary>
/// Translates the built-in ASP.NET Core Identity error messages into Arabic so
/// login and registration never surface English text to the user.
/// </summary>
public class ArabicIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() =>
        new() { Code = nameof(DefaultError), Description = "حدث خطأ غير متوقع." };

    public override IdentityError ConcurrencyFailure() =>
        new() { Code = nameof(ConcurrencyFailure), Description = "فشل الحفظ، تم تعديل البيانات من مكان آخر." };

    public override IdentityError PasswordMismatch() =>
        new() { Code = nameof(PasswordMismatch), Description = "كلمة المرور غير صحيحة." };

    public override IdentityError InvalidToken() =>
        new() { Code = nameof(InvalidToken), Description = "الرمز غير صالح." };

    public override IdentityError LoginAlreadyAssociated() =>
        new() { Code = nameof(LoginAlreadyAssociated), Description = "يوجد حساب مرتبط بهذه البيانات بالفعل." };

    public override IdentityError InvalidUserName(string? userName) =>
        new() { Code = nameof(InvalidUserName), Description = $"اسم المستخدم '{userName}' غير صالح، يُسمح بالأحرف والأرقام فقط." };

    public override IdentityError InvalidEmail(string? email) =>
        new() { Code = nameof(InvalidEmail), Description = $"البريد الإلكتروني '{email}' غير صالح." };

    public override IdentityError DuplicateUserName(string? userName) =>
        new() { Code = nameof(DuplicateUserName), Description = $"اسم المستخدم '{userName}' مستخدم بالفعل." };

    public override IdentityError DuplicateEmail(string? email) =>
        new() { Code = nameof(DuplicateEmail), Description = $"البريد الإلكتروني '{email}' مسجل بالفعل." };

    public override IdentityError InvalidRoleName(string? role) =>
        new() { Code = nameof(InvalidRoleName), Description = $"اسم الدور '{role}' غير صالح." };

    public override IdentityError DuplicateRoleName(string? role) =>
        new() { Code = nameof(DuplicateRoleName), Description = $"اسم الدور '{role}' موجود بالفعل." };

    public override IdentityError UserAlreadyHasPassword() =>
        new() { Code = nameof(UserAlreadyHasPassword), Description = "المستخدم لديه كلمة مرور بالفعل." };

    public override IdentityError UserLockoutNotEnabled() =>
        new() { Code = nameof(UserLockoutNotEnabled), Description = "خدمة الإيقاف غير مفعّلة لهذا المستخدم." };

    public override IdentityError UserAlreadyInRole(string? role) =>
        new() { Code = nameof(UserAlreadyInRole), Description = $"المستخدم مسجل بالفعل في الدور '{role}'." };

    public override IdentityError UserNotInRole(string? role) =>
        new() { Code = nameof(UserNotInRole), Description = $"المستخدم غير مسجل في الدور '{role}'." };

    public override IdentityError PasswordTooShort(int length) =>
        new() { Code = nameof(PasswordTooShort), Description = $"كلمة المرور يجب أن تكون {length} أحرف على الأقل." };

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "كلمة المرور يجب أن تحتوي على رمز خاص واحد على الأقل." };

    public override IdentityError PasswordRequiresDigit() =>
        new() { Code = nameof(PasswordRequiresDigit), Description = "كلمة المرور يجب أن تحتوي على رقم واحد على الأقل." };

    public override IdentityError PasswordRequiresLower() =>
        new() { Code = nameof(PasswordRequiresLower), Description = "كلمة المرور يجب أن تحتوي على حرف إنجليزي صغير واحد على الأقل." };

    public override IdentityError PasswordRequiresUpper() =>
        new() { Code = nameof(PasswordRequiresUpper), Description = "كلمة المرور يجب أن تحتوي على حرف إنجليزي كبير واحد على الأقل." };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        new() { Code = nameof(PasswordRequiresUniqueChars), Description = $"كلمة المرور يجب أن تحتوي على {uniqueChars} أحرف مختلفة على الأقل." };

    public override IdentityError RecoveryCodeRedemptionFailed() =>
        new() { Code = nameof(RecoveryCodeRedemptionFailed), Description = "فشل استخدام رمز الاسترداد." };
}