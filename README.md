# دار مير للأثاث الفاخر

## نظام متجر إلكتروني متكامل بـ ASP.NET Core MVC

### نظرة عامة
"دار مير" نظام إدارة متجر إلكتروني متخصص في الأثاث الفاخر ومستلزمات الديكور المنزلي.
الواجهة عربية بالكامل مع اتجاه من اليمين إلى اليسار (RTL)، والعملة الافتراضية هي **الريال اليمني (YER — ر.ي)**.

---

## التقنيات المستخدمة
- **الإطار:** ASP.NET Core MVC (.NET 9)
- **اللغة:** C#
- **الـ ORM:** Entity Framework Core 9
- **قاعدة البيانات:** SQL Server (LocalDB)
- **المصادقة:** ASP.NET Core Identity
- **الواجهة:** Razor Views + Bootstrap 5 RTL + Font Awesome 6
- **الخطوط:** Cairo و Tajawal
- **التحقق:** Data Annotations + jQuery Validation (رسائل عربية)

---

## دليل التشغيل

### المتطلبات
- .NET 9 SDK
- SQL Server (LocalDB أو نسخة كاملة)
- Visual Studio 2022 أو VS Code

### الخطوات

1. **استرجاع الحزم:**
```bash
cd DarMirFurniture
dotnet restore
```

2. **تعديل نص الاتصال** في `appsettings.json` إذا لزم الأمر

3. **إنشاء الترحيل الأولي:**
```bash
dotnet ef migrations add InitialCreate
```

4. **تطبيق الترحيل على قاعدة البيانات:**
```bash
dotnet ef database update
```

5. **تشغيل التطبيق:**
```bash
dotnet run
```

6. **الوصول:**
   - المتجر: https://localhost:5001
   - لوحة الإدارة: https://localhost:5001/Admin/Dashboard

---

## الحسابات الافتراضية

| الدور | البريد الإلكتروني | كلمة المرور |
|------|-------------------|-------------|
| مدير | admin@darmir.com | Admin@123 |
| عميل | customer@darmir.com | Customer@123 |

---

## كيانات قاعدة البيانات (10 كيانات)

1. **ApplicationUser** — مستخدم Identity موسّع (اسم، هاتف، مدينة، عنوان)
2. **Category** — فئات المنتجات
3. **Brand** — العلامات التجارية
4. **Product** — المنتج (السعر، سعر الخصم، المادة، اللون، الأبعاد، المخزون)
5. **ProductImage** — صور المنتج مع تحديد الصورة الرئيسية
6. **Cart** — سلة المشتريات الخاصة بالمستخدم
7. **CartItem** — عناصر السلة
8. **Order** — الطلب (بيانات الشحن، المجموع الفرعي، الشحن، الإجمالي، الحالة)
9. **OrderItem** — بنود الطلب
10. **Review** — مراجعات وتقييمات المنتجات

---

## العلاقات

### واحد لواحد (1:1)
- ApplicationUser ↔ Cart

### واحد لمتعدد (1:N)
- Category → Products
- Brand → Products
- Product → ProductImages
- Product → Reviews
- Product → CartItems
- Product → OrderItems
- ApplicationUser → Orders
- ApplicationUser → Reviews
- Order → OrderItems
- Cart → CartItems

---

## التعريب والعملة

- الصفحة الرئيسية والقوالب تستخدم `lang="ar" dir="rtl"` مع Bootstrap RTL.
- النصوص المشتركة مركزية في `Localization/AppText.cs`.
- رسائل أخطاء Identity معرّبة عبر `Localization/ArabicIdentityErrorDescriber.cs`.
- إعدادات العملة والتنسيق في `Localization/CurrencySettings.cs`:
  - رمز العملة: `ر.ي` وكود العملة `YER`
  - تنسيق موحّد للأسعار عبر `ToYer()` مثال: `25,000 ر.ي`
  - الشحن المجاني عند بلوغ حد معيّن، وإلا تُطبّق قيمة شحن ثابتة بالريال اليمني
- ثقافة التطبيق مضبوطة على `ar-YE` في `Program.cs` مع الحفاظ على أرقام لاتينية لضمان صحة الحسابات وربط النماذج.

---

## الوظائف

### وظائف العميل
- تصفح المنتجات مع بحث وفلترة (فئة، علامة تجارية، نطاق سعري) وترقيم صفحات
- صفحة تفاصيل المنتج مع الصور والأبعاد والمواصفات
- سلة مشتريات مع تعديل الكميات والحذف
- إتمام الطلب مع بيانات الشحن وحساب الشحن تلقائيًا
- سجل الطلبات وتتبع حالة الطلب
- إضافة مراجعة وتقييم للمنتجات
- إدارة الملف الشخصي

### وظائف الإدارة
- لوحة تحكم بإحصائيات المنتجات والطلبات والعملاء والمبيعات
- إدارة كاملة (إضافة/تعديل/حذف) للمنتجات والفئات والعلامات التجارية
- رفع وحذف صور المنتجات مع أسماء GUID وتعيين الصورة الرئيسية
- تنبيهات المخزون المنخفض عبر حد إعادة الطلب
- إدارة الطلبات وتحديث حالاتها
- إدارة المراجعات
- تقارير: المبيعات، المنتجات، العملاء

---

## هيكل المشروع
```
DarMirFurniture/
├── Areas/Admin/Controllers/    # متحكمات لوحة الإدارة
├── Areas/Admin/Views/          # واجهات لوحة الإدارة
├── Controllers/                # متحكمات المتجر
├── Data/                       # ApplicationDbContext
├── Localization/               # AppText, CurrencySettings, أخطاء Identity
├── Models/                     # الكيانات (10)
├── ViewModels/                 # نماذج العرض
├── Services/                   # منطق الأعمال
├── Views/                      # واجهات المتجر
├── Views/Shared/               # القوالب والواجهات الجزئية
├── wwwroot/css/                # site.css و admin.css (RTL)
├── wwwroot/uploads/products/   # الصور المرفوعة
├── Program.cs                  # نقطة تشغيل التطبيق
├── appsettings.json            # الإعدادات
└── DarMirFurniture.csproj      # ملف المشروع
```

---

## الأمان
- ASP.NET Core Identity مع تشفير كلمات المرور
- تصريح حسب الأدوار (مدير/عميل)
- `[ValidateAntiForgeryToken]` على جميع عمليات POST
- رفع صور آمن بأسماء GUID مع التحقق من النوع والحجم (حتى 5 ميجابايت)
- التحقق من ملكية البيانات (كل مستخدم يرى طلباته فقط)
- تحقق من الصحة على الخادم والمتصفح برسائل عربية

---

## ميزات EF Core المستخدمة
- DbContext مع إعدادات Fluent API
- خصائص التنقل (Navigation Properties)
- `Include()` و `ThenInclude()` للتحميل المسبق
- `Where()` للفلترة و `Select()` للإسقاط
- `GroupBy()`, `Sum()`, `Count()`, `Average()` للتقارير
- `OrderBy()` للترتيب
- Data Annotations للتحقق
- Migrations لإدارة إصدارات قاعدة البيانات

---

## جدول التحقق

| المطلب | الحالة | الدليل |
|--------|--------|--------|
| 10 كيانات بالضبط | ✅ | مجلد Models/ |
| علاقة 1:1 | ✅ | ApplicationUser ↔ Cart |
| علاقة 1:N | ✅ | Category→Products، Order→OrderItems |
| بذر البيانات | ✅ | Services/SeedData.cs |
| مصادقة Identity | ✅ | Program.cs، AccountController |
| تصريح بالأدوار | ✅ | `[Authorize(Roles="Admin")]` |
| CRUD المنتجات | ✅ | Admin/ProductsController |
| CRUD الفئات | ✅ | Admin/CategoriesController |
| CRUD العلامات التجارية | ✅ | Admin/BrandsController |
| رفع/حذف الصور | ✅ | ImageService + ProductService |
| البحث والفلترة | ✅ | ProductService |
| سلة المشتريات | ✅ | CartController، CartService |
| إتمام الطلب | ✅ | CartController.Checkout/PlaceOrder |
| الطلبات | ✅ | OrdersController، OrderService |
| المراجعات | ✅ | ProductsController.AddReview |
| التقارير | ✅ | Admin/ReportsController، ReportService |
| لوحة التحكم | ✅ | Admin/DashboardController |
| واجهة عربية RTL | ✅ | _Layout.cshtml، site.css، admin.css |
| العملة الريال اليمني | ✅ | Localization/CurrencySettings.cs |