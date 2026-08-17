using DarMirFurniture.Data;
using DarMirFurniture.Models;
using DarMirFurniture.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReportService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var customers = await _userManager.GetUsersInRoleAsync("Customer");

        return new DashboardViewModel
        {
            TotalProducts = await _context.Products.CountAsync(),
            TotalCustomers = customers.Count,
            TotalOrders = await _context.Orders.CountAsync(),
            TotalSales = await _context.Orders.Where(o => o.Status != OrderStatus.Cancelled).SumAsync(o => o.Total),
            PendingOrders = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Pending),
            DeliveredOrders = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Delivered),
            LowStockProducts = await _context.Products.CountAsync(p => p.StockQuantity <= p.ReorderLevel),
            TotalCategories = await _context.Categories.CountAsync(),
            TotalReviews = await _context.Reviews.CountAsync(),
            RecentOrders = await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .Select(o => new RecentOrderViewModel
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    CustomerName = o.FullName,
                    Total = o.Total,
                    Status = o.Status.ToString(),
                    OrderDate = o.OrderDate
                })
                .ToListAsync(),
            TopProducts = await _context.OrderItems
                .GroupBy(oi => new { oi.ProductId, oi.ProductName })
                .Select(g => new TopProductViewModel
                {
                    Id = g.Key.ProductId,
                    Name = g.Key.ProductName,
                    TotalSold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync()
        };
    }

    public async Task<SalesReportViewModel> GetSalesReportAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Orders.Where(o => o.Status != OrderStatus.Cancelled);

        if (startDate.HasValue)
            query = query.Where(o => o.OrderDate >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(o => o.OrderDate <= endDate.Value);

        var orders = await query.ToListAsync();

        return new SalesReportViewModel
        {
            TotalSales = orders.Sum(o => o.Total),
            TotalOrders = orders.Count,
            AverageOrderValue = orders.Any() ? orders.Average(o => o.Total) : 0,
            StartDate = startDate,
            EndDate = endDate,
            SalesByDate = orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesByDateViewModel
                {
                    Date = g.Key,
                    Sales = g.Sum(o => o.Total),
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.Date)
                .ToList()
        };
    }

    public async Task<ProductReportViewModel> GetProductReportAsync()
    {
        return new ProductReportViewModel
        {
            BestSellingProducts = await _context.OrderItems
                .GroupBy(oi => new { oi.ProductId, oi.ProductName })
                .Select(g => new TopProductViewModel
                {
                    Id = g.Key.ProductId,
                    Name = g.Key.ProductName,
                    TotalSold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(10)
                .ToListAsync(),

            PopularCategories = await _context.Products
                .Where(p => p.CategoryId != null)
                .Include(p => p.Category)
                .Include(p => p.OrderItems)
                .GroupBy(p => p.Category!.Name)
                .Select(g => new CategorySalesViewModel
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count(),
                    TotalSold = g.Sum(p => p.OrderItems.Sum(oi => oi.Quantity))
                })
                .OrderByDescending(x => x.TotalSold)
                .ToListAsync(),

            LowStockProducts = await _context.Products
                .Where(p => p.StockQuantity <= p.ReorderLevel)
                .Select(p => new LowStockViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    CurrentStock = p.StockQuantity,
                    ReorderLevel = p.ReorderLevel
                })
                .ToListAsync(),

            TotalAvailableProducts = await _context.Products.CountAsync(p => p.IsAvailable),
            TotalUnavailableProducts = await _context.Products.CountAsync(p => !p.IsAvailable)
        };
    }

    public async Task<CustomerReportViewModel> GetCustomerReportAsync()
    {
        var customers = await _userManager.GetUsersInRoleAsync("Customer");

        return new CustomerReportViewModel
        {
            TotalCustomers = customers.Count,
            TopCustomers = await _context.Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .GroupBy(o => new { o.UserId, o.User.FirstName, o.User.LastName, o.User.Email })
                .Select(g => new CustomerOrderViewModel
                {
                    CustomerId = g.Key.UserId,
                    CustomerName = g.Key.FirstName + " " + g.Key.LastName,
                    Email = g.Key.Email ?? "",
                    OrderCount = g.Count(),
                    TotalSpent = g.Sum(o => o.Total)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(10)
                .ToListAsync(),

            RecentCustomers = customers
                .OrderByDescending(c => c.CreatedAt)
                .Take(10)
                .Select(c => new RecentCustomerViewModel
                {
                    Id = c.Id,
                    Name = $"{c.FirstName} {c.LastName}",
                    Email = c.Email ?? "",
                    JoinedDate = c.CreatedAt
                })
                .ToList()
        };
    }
}