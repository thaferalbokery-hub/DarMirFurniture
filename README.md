# DarMir Luxury Furniture & Home Decor

## Complete ASP.NET Core MVC E-Commerce System

### Project Overview
DarMir is a full-featured e-commerce management system specialized in luxury furniture and home decoration products. Built with ASP.NET Core MVC (.NET 9), Entity Framework Core, SQL Server, and ASP.NET Core Identity.

---

## Technologies Used
- **Framework:** ASP.NET Core MVC (.NET 9)
- **Language:** C#
- **ORM:** Entity Framework Core 9
- **Database:** SQL Server (LocalDB)
- **Authentication:** ASP.NET Core Identity
- **Frontend:** Razor Views, Bootstrap 5, Font Awesome 6
- **Validation:** Data Annotations + jQuery Validation

---

## Installation Guide

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Steps

1. **Clone/Download the project**

2. **Restore packages:**
```bash
cd DarMirFurniture
dotnet restore
```

3. **Update connection string** in `appsettings.json` if needed

4. **Create database migrations:**
```bash
dotnet ef migrations add InitialCreate
```

5. **Apply migrations:**
```bash
dotnet ef database update
```

6. **Run the application:**
```bash
dotnet run
```

7. **Access the application:**
   - Store: https://localhost:5001
   - Admin: https://localhost:5001/Admin/Dashboard

---

## Default Accounts

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@darmir.com | Admin@123 |
| Customer | customer@darmir.com | Customer@123 |

---

## Database Models (18 Models)

1. ApplicationUser - Extended Identity user
2. UserProfile - One-to-One with ApplicationUser
3. Product - Main product entity
4. Category - Product categories
5. Brand - Product brands
6. FurnitureType - Types of furniture
7. Material - Product materials
8. Color - Available colors
9. ProductCategory - Many-to-Many junction
10. ProductImage - Product images
11. ProductVariant - Product variants (Color + Material)
12. Inventory - Stock management
13. Cart - Shopping cart
14. CartItem - Cart items
15. Order - Customer orders
16. OrderItem - Order line items
17. Review - Product reviews
18. Favorite - User favorites
19. Address - User addresses

---

## Database Relationships

### One-to-One
- ApplicationUser ↔ UserProfile
- ApplicationUser ↔ Cart
- Product ↔ Inventory

### One-to-Many
- Brand → Products
- FurnitureType → Products
- Material → Products
- Product → ProductImages
- Product → ProductVariants
- Product → Reviews
- ApplicationUser → Orders
- ApplicationUser → Reviews
- Order → OrderItems
- Cart → CartItems

### Many-to-Many
- Product ↔ Category (via ProductCategory junction entity)
- ProductVariant connects Product + Color + Material

---

## Features

### Customer Features
- Browse products with filtering and search
- View product details with images, dimensions, variants
- Add/remove favorites
- Shopping cart with quantity management
- Checkout with shipping information
- Order history and tracking
- Product reviews and ratings
- Profile management

### Admin Features
- Dashboard with statistics
- Full CRUD for: Products, Categories, Brands, Furniture Types, Materials, Colors
- Image upload/delete with GUID naming
- Inventory management with low-stock alerts
- Order management with status updates
- Review management
- Sales, Product, and Customer reports

---

## Project Structure
```
DarMirFurniture/
├── Areas/Admin/Controllers/    # Admin controllers
├── Areas/Admin/Views/          # Admin Razor views
├── Controllers/                # Customer controllers
├── Data/                       # DbContext
├── Models/                     # Entity models
├── ViewModels/                 # View models / DTOs
├── Services/                   # Business logic services
├── Views/                      # Customer Razor views
├── Views/Shared/               # Partial views, layouts
├── Migrations/                 # EF Core migrations
├── wwwroot/css/                # Stylesheets
├── wwwroot/js/                 # JavaScript
├── wwwroot/uploads/            # Uploaded images
├── Program.cs                  # Application entry point
├── appsettings.json            # Configuration
└── DarMirFurniture.csproj      # Project file
```

---

## Security Features
- ASP.NET Core Identity with password hashing
- Role-based authorization (Admin/Customer)
- [ValidateAntiForgeryToken] on all POST actions
- Secure image upload with GUID filenames
- File type and size validation
- Ownership validation (users can only access their own data)
- Server-side and client-side validation

---

## EF Core Features Used
- DbContext with Fluent API configuration
- Navigation Properties
- Include() and ThenInclude() for eager loading
- Where() for row-level filtering
- Select() for column-level projection
- GroupBy(), Sum(), Count(), Average() for reports
- OrderBy() for sorting
- Data Annotations for validation
- Migrations for database versioning

---

## Class Diagram

```
ApplicationUser (IdentityUser)
├── 1:1 → UserProfile
├── 1:1 → Cart → CartItems → Product
├── 1:N → Orders → OrderItems → Product
├── 1:N → Reviews → Product
└── 1:N → Favorites → Product

Product
├── N:1 → Brand
├── N:1 → FurnitureType
├── N:1 → Material
├── M:N → Categories (via ProductCategory)
├── 1:N → ProductImages
├── 1:N → ProductVariants → Color, Material
├── 1:1 → Inventory
├── 1:N → Reviews
└── 1:N → Favorites
```

---

## Audit Verification

| Requirement | Status | Evidence |
|-------------|--------|----------|
| 18 Models | ✅ | Models/ folder |
| 1:1 Relationship | ✅ | User↔Profile, User↔Cart, Product↔Inventory |
| 1:N Relationship | ✅ | Brand→Products, Order→OrderItems, etc. |
| M:N Relationship | ✅ | Product↔Category via ProductCategory |
| Seed Data | ✅ | Services/SeedData.cs |
| Identity Auth | ✅ | Program.cs, AccountController |
| Role Authorization | ✅ | [Authorize(Roles="Admin")] |
| Product CRUD | ✅ | Admin/ProductsController |
| Category CRUD | ✅ | Admin/CategoriesController |
| Brand CRUD | ✅ | Admin/BrandsController |
| FurnitureType CRUD | ✅ | Admin/FurnitureTypesController |
| Material CRUD | ✅ | Admin/MaterialsController |
| Color CRUD | ✅ | Admin/ColorsController |
| Image Upload/Delete | ✅ | ImageService, GUID naming |
| Search | ✅ | ProductService.GetFilteredProductsAsync |
| Row Filtering (Where) | ✅ | ProductService filters |
| Column Projection (Select) | ✅ | ProductListItemViewModel |
| Include/ThenInclude | ✅ | ProductService, CartService, OrderService |
| Data Annotations | ✅ | All models |
| Tag Helpers | ✅ | All views |
| ViewBag/ViewData | ✅ | Controllers + Views |
| Partial Views | ✅ | _Navbar, _Footer, _ProductCard |
| Shopping Cart | ✅ | CartController, CartService |
| Checkout | ✅ | CartController.Checkout/PlaceOrder |
| Orders | ✅ | OrdersController, OrderService |
| Inventory | ✅ | InventoryController, InventoryService |
| Favorites | ✅ | FavoritesController, FavoriteService |
| Reviews | ✅ | ProductsController.AddReview |
| Reports | ✅ | ReportsController, ReportService |
| Admin Dashboard | ✅ | DashboardController |
| EF Core Migrations | ✅ | Ready for `dotnet ef migrations add` |
| SQL Server | ✅ | appsettings.json connection string |