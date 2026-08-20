using DarMirFurniture.Data;
using DarMirFurniture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();

        // Seed Roles
        string[] roles = { "Admin", "Customer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminEmail = "admin@darmir.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "مدير",
                LastName = "النظام",
                Phone = "777000000",
                City = "صنعاء",
                Address = "شارع حدة، صنعاء",
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        // Seed Customer User
        var customerEmail = "customer@darmir.com";
        if (await userManager.FindByEmailAsync(customerEmail) == null)
        {
            var customer = new ApplicationUser
            {
                UserName = customerEmail,
                Email = customerEmail,
                FirstName = "أحمد",
                LastName = "الشميري",
                Phone = "711111111",
                City = "عدن",
                Address = "شارع المعلا، عدن",
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await userManager.CreateAsync(customer, "Customer@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(customer, "Customer");
            }
        }

        await context.SaveChangesAsync();

        // Seed Categories
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Name = "غرفة الجلوس", Description = "أثاث فاخر لغرف الجلوس", IsActive = true, DisplayOrder = 1 },
                new() { Name = "غرفة النوم", Description = "أثاث راقٍ لغرف النوم", IsActive = true, DisplayOrder = 2 },
                new() { Name = "غرفة الطعام", Description = "طقم طعام أنيق وعصري", IsActive = true, DisplayOrder = 3 },
                new() { Name = "أثاث المكاتب", Description = "أثاث مكتبي احترافي", IsActive = true, DisplayOrder = 4 },
                new() { Name = "الأثاث الخارجي", Description = "أثاث خارجي مقاوم للعوامل الجوية", IsActive = true, DisplayOrder = 5 },
                new() { Name = "الإضاءة", Description = "وحدات إضاءة بتصاميم مميزة", IsActive = true, DisplayOrder = 6 },
                new() { Name = "مستلزمات المنزل", Description = "قطع ديكور وإكسسوارات منزلية", IsActive = true, DisplayOrder = 7 },
                new() { Name = "وحدات التخزين", Description = "خزائن وحلول تخزين عملية", IsActive = true, DisplayOrder = 8 }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        // Seed Brands
        if (!await context.Brands.AnyAsync())
        {
            var brands = new List<Brand>
            {
                new() { Name = "مجموعة دار مير", Description = "مجموعتنا الحصرية الفاخرة", IsActive = true },
                new() { Name = "الأثاث الملكي", Description = "تصاميم كلاسيكية ملكية", IsActive = true },
                new() { Name = "العيش العصري", Description = "أثاث عصري معاصر", IsActive = true },
                new() { Name = "خشب الحرفيين", Description = "أثاث خشبي مصنوع يدويًا", IsActive = true },
                new() { Name = "أناقة المنزل", Description = "ديكورات منزلية أنيقة", IsActive = true }
            };
            context.Brands.AddRange(brands);
            await context.SaveChangesAsync();
        }

        // Seed Products - all prices are in Yemeni Riyal (YER)
        if (!await context.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new()
                {
                    Name = "كنبة تشسترفيلد ملكية",
                    Description = "كنبة تشسترفيلد فاخرة مصنوعة من الجلد الطبيعي بهيكل من الخشب الصلب. مثالية لغرف الجلوس الأنيقة.",
                    Price = 875_000,
                    DiscountPrice = 769_000,
                    Material = "جلد طبيعي",
                    Color = "بني",
                    Width = 220, Height = 85, Depth = 95, Weight = 65,
                    CategoryId = 1, BrandId = 1,
                    StockQuantity = 25, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "طاولة طعام من خشب الجوز",
                    Description = "طاولة طعام أنيقة مصنوعة من خشب الجوز الفاخر بتصميم عصري انسيابي. تتسع لثمانية أشخاص بكل أريحية.",
                    Price = 595_000,
                    Material = "خشب الجوز",
                    Color = "بني غامق",
                    Width = 200, Height = 76, Depth = 100, Weight = 45,
                    CategoryId = 3, BrandId = 4,
                    StockQuantity = 15, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "سرير مخملي كينج",
                    Description = "سرير فاخر بمقاس كينج بتنجيد مخملي وهيكل بلمسات ذهبية. قطعة مميزة تُضفي رقيًا على غرفة النوم.",
                    Price = 1_050_000,
                    DiscountPrice = 945_000,
                    Material = "قماش مخملي",
                    Color = "رمادي",
                    Width = 200, Height = 140, Depth = 220, Weight = 80,
                    CategoryId = 2, BrandId = 2,
                    StockQuantity = 10, ReorderLevel = 3,
                    IsAvailable = true, IsFeatured = true, IsNew = false
                },
                new()
                {
                    Name = "مكتب تنفيذي فخم",
                    Description = "مكتب تنفيذي راقٍ بتشطيب خشب البلوط مع نظام مدمج لتنظيم الأسلاك. مناسب لبيئة العمل الاحترافية.",
                    Price = 455_000,
                    Material = "خشب البلوط",
                    Color = "بلوط طبيعي",
                    Width = 180, Height = 76, Depth = 80, Weight = 35,
                    CategoryId = 4, BrandId = 3,
                    StockQuantity = 20, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = false, IsNew = true
                },
                new()
                {
                    Name = "طاولة قهوة رخامية",
                    Description = "طاولة قهوة مذهلة بسطح من الرخام الطبيعي وأرجل معدنية ذهبية. قطعة محورية مثالية لغرفة الجلوس.",
                    Price = 315_000,
                    DiscountPrice = 279_000,
                    Material = "رخام ومعدن",
                    Color = "أبيض/ذهبي",
                    Width = 120, Height = 45, Depth = 60, Weight = 25,
                    CategoryId = 1, BrandId = 5,
                    StockQuantity = 30, ReorderLevel = 8,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "مكتبة كتب كلاسيكية",
                    Description = "مكتبة كتب عالية مصنوعة من الخشب الصلب مع أرفف قابلة للتعديل. تجمع بين العملية والأناقة.",
                    Price = 224_000,
                    Material = "خشب صلب",
                    Color = "جوزي",
                    Width = 100, Height = 200, Depth = 35, Weight = 30,
                    CategoryId = 8, BrandId = 4,
                    StockQuantity = 18, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = false, IsNew = false
                },
                new()
                {
                    Name = "مصباح أرضي بتصميم مميز",
                    Description = "مصباح أرضي عصري بذراع قابل للتعديل وإضاءة LED دافئة. يضيف لمسة أجواء هادئة لأي غرفة.",
                    Price = 126_000,
                    DiscountPrice = 105_000,
                    Material = "معدن وزجاج",
                    Color = "أسود/ذهبي",
                    Width = 40, Height = 170, Depth = 40, Weight = 8,
                    CategoryId = 6, BrandId = 5,
                    StockQuantity = 40, ReorderLevel = 10,
                    IsAvailable = true, IsFeatured = false, IsNew = true
                },
                new()
                {
                    Name = "وحدة تلفزيون فاخرة",
                    Description = "وحدة تلفزيون واسعة بتشطيب خشب الجوز مع أدراج تخزين مخفية. تتحمل شاشات حتى 75 بوصة.",
                    Price = 385_000,
                    Material = "خشب الجوز",
                    Color = "جوزي غامق",
                    Width = 200, Height = 55, Depth = 45, Weight = 40,
                    CategoryId = 1, BrandId = 3,
                    StockQuantity = 12, ReorderLevel = 4,
                    IsAvailable = true, IsFeatured = true, IsNew = false
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // Add Product Images
            var productImages = new List<ProductImage>
            {
                new() { ProductId = 1, ImageUrl = "/uploads/products/sofa-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "كنبة تشسترفيلد ملكية" },
                new() { ProductId = 2, ImageUrl = "/uploads/products/table-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "طاولة طعام من خشب الجوز" },
                new() { ProductId = 3, ImageUrl = "/uploads/products/bed-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "سرير مخملي كينج" },
                new() { ProductId = 4, ImageUrl = "/uploads/products/desk-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "مكتب تنفيذي فخم" },
                new() { ProductId = 5, ImageUrl = "/uploads/products/coffee-table-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "طاولة قهوة رخامية" },
                new() { ProductId = 6, ImageUrl = "/uploads/products/bookshelf-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "مكتبة كتب كلاسيكية" },
                new() { ProductId = 7, ImageUrl = "/uploads/products/lamp-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "مصباح أرضي بتصميم مميز" },
                new() { ProductId = 8, ImageUrl = "/uploads/products/tv-unit-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "وحدة تلفزيون فاخرة" }
            };
            context.ProductImages.AddRange(productImages);
            await context.SaveChangesAsync();

            // Add sample orders and reviews
            var customer = await userManager.FindByEmailAsync("customer@darmir.com");
            if (customer != null)
            {
                // Create a sample order (amounts in YER)
                var order = new Order
                {
                    UserId = customer.Id,
                    OrderNumber = "DM202608001",
                    FullName = "أحمد الشميري",
                    Phone = "711111111",
                    City = "عدن",
                    Address = "شارع المعلا، عدن",
                    Subtotal = 1_643_000,
                    ShippingCost = 0,
                    Total = 1_643_000,
                    Status = OrderStatus.Delivered,
                    OrderDate = DateTime.UtcNow.AddDays(-15)
                };
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                // Add order items
                var orderItems = new List<OrderItem>
                {
                    new() { OrderId = order.Id, ProductId = 1, ProductName = "كنبة تشسترفيلد ملكية", Quantity = 1, UnitPrice = 769_000, Subtotal = 769_000 },
                    new() { OrderId = order.Id, ProductId = 2, ProductName = "طاولة طعام من خشب الجوز", Quantity = 1, UnitPrice = 595_000, Subtotal = 595_000 },
                    new() { OrderId = order.Id, ProductId = 5, ProductName = "طاولة قهوة رخامية", Quantity = 1, UnitPrice = 279_000, Subtotal = 279_000 }
                };
                context.OrderItems.AddRange(orderItems);

                // Add sample reviews
                var reviews = new List<Review>
                {
                    new() { ProductId = 1, UserId = customer.Id, Rating = 5, Comment = "كنبة رائعة بكل المقاييس! جودة الجلد استثنائية.", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                    new() { ProductId = 2, UserId = customer.Id, Rating = 4, Comment = "طاولة طعام جميلة، وتشطيب خشب الجوز أنيق جدًا.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                    new() { ProductId = 5, UserId = customer.Id, Rating = 5, Comment = "سطح الرخام يخطف الأنظار، مثالي لغرفة الجلوس.", CreatedAt = DateTime.UtcNow.AddDays(-2) }
                };
                context.Reviews.AddRange(reviews);
                await context.SaveChangesAsync();
            }
        }
    }
}