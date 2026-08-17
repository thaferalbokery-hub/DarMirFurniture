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
                FirstName = "Admin",
                LastName = "DarMir",
                Phone = "0500000000",
                City = "Riyadh",
                Address = "King Fahd Road, Riyadh",
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
                FirstName = "Ahmed",
                LastName = "Customer",
                Phone = "0511111111",
                City = "Jeddah",
                Address = "Palestine Street, Jeddah",
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
                new() { Name = "Living Room", Description = "Luxury living room furniture", IsActive = true, DisplayOrder = 1 },
                new() { Name = "Bedroom", Description = "Premium bedroom furniture", IsActive = true, DisplayOrder = 2 },
                new() { Name = "Dining Room", Description = "Elegant dining room sets", IsActive = true, DisplayOrder = 3 },
                new() { Name = "Office Furniture", Description = "Professional office furniture", IsActive = true, DisplayOrder = 4 },
                new() { Name = "Outdoor Furniture", Description = "Durable outdoor furniture", IsActive = true, DisplayOrder = 5 },
                new() { Name = "Lighting", Description = "Designer lighting", IsActive = true, DisplayOrder = 6 },
                new() { Name = "Home Accessories", Description = "Decorative accessories", IsActive = true, DisplayOrder = 7 },
                new() { Name = "Storage", Description = "Cabinets and storage solutions", IsActive = true, DisplayOrder = 8 }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        // Seed Brands
        if (!await context.Brands.AnyAsync())
        {
            var brands = new List<Brand>
            {
                new() { Name = "DarMir Collection", Description = "Our exclusive luxury collection", IsActive = true },
                new() { Name = "Royal Furniture", Description = "Classic royal designs", IsActive = true },
                new() { Name = "Modern Living", Description = "Contemporary modern furniture", IsActive = true },
                new() { Name = "Artisan Wood", Description = "Handcrafted wooden furniture", IsActive = true },
                new() { Name = "Elegance Home", Description = "Elegant home decor", IsActive = true }
            };
            context.Brands.AddRange(brands);
            await context.SaveChangesAsync();
        }

        // Seed Products
        if (!await context.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new()
                {
                    Name = "Royal Chesterfield Sofa",
                    Description = "A luxurious Chesterfield sofa crafted with genuine leather and solid wood frame. Perfect for elegant living rooms.",
                    Price = 12500,
                    DiscountPrice = 10999,
                    Material = "Genuine Leather",
                    Color = "Brown",
                    Width = 220, Height = 85, Depth = 95, Weight = 65,
                    CategoryId = 1, BrandId = 1,
                    StockQuantity = 25, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "Modern Walnut Dining Table",
                    Description = "Elegant dining table made from premium walnut wood with a sleek modern design. Seats 8 comfortably.",
                    Price = 8500,
                    Material = "Walnut Wood",
                    Color = "Dark Brown",
                    Width = 200, Height = 76, Depth = 100, Weight = 45,
                    CategoryId = 3, BrandId = 4,
                    StockQuantity = 15, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "Velvet King Size Bed",
                    Description = "Luxurious king size bed with velvet upholstery and gold-accented frame. A statement piece for any bedroom.",
                    Price = 15000,
                    DiscountPrice = 13500,
                    Material = "Velvet Fabric",
                    Color = "Gray",
                    Width = 200, Height = 140, Depth = 220, Weight = 80,
                    CategoryId = 2, BrandId = 2,
                    StockQuantity = 10, ReorderLevel = 3,
                    IsAvailable = true, IsFeatured = true, IsNew = false
                },
                new()
                {
                    Name = "Executive Office Desk",
                    Description = "Premium executive desk with oak finish and built-in cable management. Perfect for the modern professional.",
                    Price = 6500,
                    Material = "Oak Wood",
                    Color = "Natural Oak",
                    Width = 180, Height = 76, Depth = 80, Weight = 35,
                    CategoryId = 4, BrandId = 3,
                    StockQuantity = 20, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = false, IsNew = true
                },
                new()
                {
                    Name = "Marble Coffee Table",
                    Description = "Stunning coffee table with natural marble top and gold metal legs. A perfect centerpiece for your living room.",
                    Price = 4500,
                    DiscountPrice = 3999,
                    Material = "Marble & Metal",
                    Color = "White/Gold",
                    Width = 120, Height = 45, Depth = 60, Weight = 25,
                    CategoryId = 1, BrandId = 5,
                    StockQuantity = 30, ReorderLevel = 8,
                    IsAvailable = true, IsFeatured = true, IsNew = true
                },
                new()
                {
                    Name = "Classic Bookshelf",
                    Description = "Tall bookshelf made from solid wood with adjustable shelves. Combines functionality with elegance.",
                    Price = 3200,
                    Material = "Solid Wood",
                    Color = "Walnut",
                    Width = 100, Height = 200, Depth = 35, Weight = 30,
                    CategoryId = 8, BrandId = 4,
                    StockQuantity = 18, ReorderLevel = 5,
                    IsAvailable = true, IsFeatured = false, IsNew = false
                },
                new()
                {
                    Name = "Designer Floor Lamp",
                    Description = "Modern floor lamp with adjustable arm and warm LED lighting. Adds ambiance to any room.",
                    Price = 1800,
                    DiscountPrice = 1500,
                    Material = "Metal & Glass",
                    Color = "Black/Gold",
                    Width = 40, Height = 170, Depth = 40, Weight = 8,
                    CategoryId = 6, BrandId = 5,
                    StockQuantity = 40, ReorderLevel = 10,
                    IsAvailable = true, IsFeatured = false, IsNew = true
                },
                new()
                {
                    Name = "Luxury TV Unit",
                    Description = "Wide TV unit with walnut finish and hidden storage compartments. Supports TVs up to 75 inches.",
                    Price = 5500,
                    Material = "Walnut Wood",
                    Color = "Dark Walnut",
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
                new() { ProductId = 1, ImageUrl = "/uploads/products/sofa-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Royal Chesterfield Sofa" },
                new() { ProductId = 2, ImageUrl = "/uploads/products/table-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Modern Walnut Dining Table" },
                new() { ProductId = 3, ImageUrl = "/uploads/products/bed-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Velvet King Size Bed" },
                new() { ProductId = 4, ImageUrl = "/uploads/products/desk-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Executive Office Desk" },
                new() { ProductId = 5, ImageUrl = "/uploads/products/coffee-table-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Marble Coffee Table" },
                new() { ProductId = 6, ImageUrl = "/uploads/products/bookshelf-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Classic Bookshelf" },
                new() { ProductId = 7, ImageUrl = "/uploads/products/lamp-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Designer Floor Lamp" },
                new() { ProductId = 8, ImageUrl = "/uploads/products/tv-unit-1.jpg", IsPrimary = true, DisplayOrder = 0, AltText = "Luxury TV Unit" }
            };
            context.ProductImages.AddRange(productImages);
            await context.SaveChangesAsync();

            // Add sample orders and reviews
            var customer = await userManager.FindByEmailAsync("customer@darmir.com");
            if (customer != null)
            {
                // Create a sample order
                var order = new Order
                {
                    UserId = customer.Id,
                    OrderNumber = "DM202608001",
                    FullName = "Ahmed Customer",
                    Phone = "0511111111",
                    City = "Jeddah",
                    Address = "Palestine Street, Jeddah",
                    Subtotal = 23499,
                    ShippingCost = 0,
                    Total = 23499,
                    Status = OrderStatus.Delivered,
                    OrderDate = DateTime.UtcNow.AddDays(-15)
                };
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                // Add order items
                var orderItems = new List<OrderItem>
                {
                    new() { OrderId = order.Id, ProductId = 1, ProductName = "Royal Chesterfield Sofa", Quantity = 1, UnitPrice = 10999, Subtotal = 10999 },
                    new() { OrderId = order.Id, ProductId = 2, ProductName = "Modern Walnut Dining Table", Quantity = 1, UnitPrice = 8500, Subtotal = 8500 },
                    new() { OrderId = order.Id, ProductId = 5, ProductName = "Marble Coffee Table", Quantity = 1, UnitPrice = 3999, Subtotal = 3999 }
                };
                context.OrderItems.AddRange(orderItems);

                // Add sample reviews
                var reviews = new List<Review>
                {
                    new() { ProductId = 1, UserId = customer.Id, Rating = 5, Comment = "Absolutely stunning sofa! The leather quality is exceptional.", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                    new() { ProductId = 2, UserId = customer.Id, Rating = 4, Comment = "Beautiful dining table. The walnut finish is gorgeous.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                    new() { ProductId = 5, UserId = customer.Id, Rating = 5, Comment = "The marble top is breathtaking. Perfect for our living room.", CreatedAt = DateTime.UtcNow.AddDays(-2) }
                };
                context.Reviews.AddRange(reviews);
                await context.SaveChangesAsync();
            }
        }
    }
}