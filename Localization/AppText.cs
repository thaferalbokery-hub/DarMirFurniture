using DarMirFurniture.Models;

namespace DarMirFurniture.Localization;

/// <summary>
/// Shared Arabic user-interface strings. Text that appears in more than one
/// place lives here so it is translated once and stays consistent.
/// </summary>
public static class AppText
{
    // Brand
    public const string SiteName = "دار مير";
    public const string SiteFullName = "دار مير للأثاث الفاخر";
    public const string SiteTagline = "أثاث فاخر وديكور منزلي";
    public const string AdminPanel = "لوحة تحكم دار مير";

    // Navigation
    public const string Home = "الرئيسية";
    public const string Products = "المنتجات";
    public const string About = "من نحن";
    public const string Contact = "اتصل بنا";
    public const string Dashboard = "لوحة التحكم";
    public const string MyOrders = "طلباتي";
    public const string MyProfile = "ملفي الشخصي";
    public const string Login = "تسجيل الدخول";
    public const string Register = "إنشاء حساب";
    public const string Logout = "تسجيل الخروج";
    public const string Cart = "سلة المشتريات";
    public const string ViewStore = "عرض المتجر";

    // Common labels
    public const string Free = "مجاني";
    public const string Subtotal = "المجموع الفرعي";
    public const string Shipping = "الشحن";
    public const string Total = "الإجمالي";
    public const string Price = "السعر";
    public const string Quantity = "الكمية";
    public const string Product = "المنتج";
    public const string Category = "الفئة";
    public const string Brand = "العلامة التجارية";
    public const string Customer = "العميل";
    public const string Status = "الحالة";
    public const string Date = "التاريخ";
    public const string Actions = "الإجراءات";
    public const string Active = "مفعّل";
    public const string Inactive = "غير مفعّل";
    public const string Available = "متوفر";
    public const string Unavailable = "غير متوفر";
    public const string New = "جديد";
    public const string Featured = "مميز";
    public const string OutOfStock = "غير متوفر بالمخزون";
    public const string Save = "حفظ";
    public const string Cancel = "إلغاء";
    public const string Update = "تحديث";
    public const string Create = "إضافة";
    public const string Details = "التفاصيل";
    public const string Search = "بحث";
    public const string Clear = "إعادة تعيين";
    public const string OrderNumber = "رقم الطلب";
    public const string Reviews = "المراجعات";
    public const string Orders = "الطلبات";
    public const string Categories = "الفئات";
    public const string Brands = "العلامات التجارية";
    public const string Reports = "التقارير";
    public const string ContinueShopping = "متابعة التسوق";
    public const string BrowseProducts = "تصفح المنتجات";
    public const string AddToCart = "إضافة إلى السلة";
    public const string Checkout = "إتمام الطلب";

    // Confirmation dialogs
    public const string ConfirmDelete = "هل أنت متأكد من الحذف؟";
    public const string ConfirmDeleteProduct = "هل تريد حذف هذا المنتج؟";
    public const string ConfirmDeleteCategory = "هل تريد حذف هذه الفئة؟";
    public const string ConfirmDeleteBrand = "هل تريد حذف هذه العلامة التجارية؟";
    public const string ConfirmDeleteReview = "هل تريد حذف هذه المراجعة؟";
    public const string ConfirmDeleteImage = "هل تريد حذف هذه الصورة؟";

    // Success messages
    public const string ProductAddedToCart = "تمت إضافة المنتج إلى السلة";
    public const string ProductRemovedFromCart = "تم حذف المنتج من السلة";
    public const string CartEmpty = "السلة فارغة";
    public const string OrderCreated = "تم إنشاء الطلب بنجاح";
    public const string ReviewCreated = "تم إضافة المراجعة بنجاح";
    public const string ReviewDeleted = "تم حذف المراجعة بنجاح";
    public const string AccountCreated = "تم إنشاء الحساب بنجاح";
    public const string ProfileUpdated = "تم تحديث الملف الشخصي بنجاح";
    public const string ProductCreated = "تم إنشاء المنتج بنجاح";
    public const string ProductUpdated = "تم تحديث المنتج بنجاح";
    public const string ProductDeleted = "تم حذف المنتج بنجاح";
    public const string ImageDeleted = "تم حذف الصورة بنجاح";
    public const string CategoryCreated = "تم إنشاء الفئة بنجاح";
    public const string CategoryUpdated = "تم تحديث الفئة بنجاح";
    public const string CategoryDeleted = "تم حذف الفئة بنجاح";
    public const string BrandCreated = "تم إنشاء العلامة التجارية بنجاح";
    public const string BrandUpdated = "تم تحديث العلامة التجارية بنجاح";
    public const string BrandDeleted = "تم حذف العلامة التجارية بنجاح";
    public const string OrderStatusUpdated = "تم تحديث حالة الطلب بنجاح";

    // Error messages
    public const string InvalidLogin = "بيانات تسجيل الدخول غير صحيحة";
    public const string InvalidImage = "ملف الصورة غير صالح";
    public const string EmptyCartError = "السلة فارغة، لا يمكن إتمام الطلب";

    /// <summary>Message shown when a product does not have enough stock.</summary>
    public static string InsufficientStock(string productName) =>
        $"الكمية المتوفرة غير كافية للمنتج: {productName}";

    // Page titles
    public const string HomePageTitle = "دار مير - أثاث فاخر وديكور منزلي";
    public const string ProductsPageTitle = "منتجاتنا";
    public const string AboutPageTitle = "من نحن";
    public const string ContactPageTitle = "اتصل بنا";
    public const string AccessDenied = "لا يوجد صلاحية للوصول";
    public const string OrderConfirmationTitle = "تأكيد الطلب";
    public const string ManageProducts = "إدارة المنتجات";
    public const string ManageCategories = "إدارة الفئات";
    public const string ManageBrands = "إدارة العلامات التجارية";
    public const string ManageOrders = "إدارة الطلبات";
    public const string ManageReviews = "إدارة المراجعات";
    public const string SalesReport = "تقرير المبيعات";
    public const string ProductsReport = "تقرير المنتجات";
    public const string CustomersReport = "تقرير العملاء";
    public const string AdminDashboard = "لوحة تحكم الإدارة";
    public const string CreateProduct = "إضافة منتج";
    public const string EditProduct = "تعديل منتج";
    public const string CreateCategory = "إضافة فئة";
    public const string EditCategory = "تعديل فئة";
    public const string CreateBrand = "إضافة علامة تجارية";
    public const string EditBrand = "تعديل علامة تجارية";

    /// <summary>Standard Arabic date format used across the UI.</summary>
    public const string DateFormat = "yyyy/MM/dd";

    /// <summary>Standard Arabic date and time format used across the UI.</summary>
    public const string DateTimeFormat = "yyyy/MM/dd HH:mm";
}

/// <summary>Arabic display helpers for the <see cref="OrderStatus"/> enum.</summary>
public static class OrderStatusText
{
    public static string ToArabic(this OrderStatus status) => status switch
    {
        OrderStatus.Pending => "قيد الانتظار",
        OrderStatus.Confirmed => "مؤكد",
        OrderStatus.Processing => "قيد التجهيز",
        OrderStatus.Shipped => "تم الشحن",
        OrderStatus.Delivered => "تم التسليم",
        OrderStatus.Cancelled => "ملغي",
        _ => status.ToString()
    };

    public static string BadgeClass(this OrderStatus status) => status switch
    {
        OrderStatus.Pending => "bg-warning text-dark",
        OrderStatus.Confirmed => "bg-info",
        OrderStatus.Processing => "bg-info",
        OrderStatus.Shipped => "bg-primary",
        OrderStatus.Delivered => "bg-success",
        OrderStatus.Cancelled => "bg-danger",
        _ => "bg-secondary"
    };
}