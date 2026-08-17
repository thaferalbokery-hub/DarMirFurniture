namespace DarMirFurniture.ViewModels;

public class SalesReportViewModel
{
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List<SalesByDateViewModel> SalesByDate { get; set; } = new();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class SalesByDateViewModel
{
    public DateTime Date { get; set; }
    public decimal Sales { get; set; }
    public int OrderCount { get; set; }
}

public class ProductReportViewModel
{
    public List<TopProductViewModel> BestSellingProducts { get; set; } = new();
    public List<CategorySalesViewModel> PopularCategories { get; set; } = new();
    public List<LowStockViewModel> LowStockProducts { get; set; } = new();
    public int TotalAvailableProducts { get; set; }
    public int TotalUnavailableProducts { get; set; }
}

public class CategorySalesViewModel
{
    public string CategoryName { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public int TotalSold { get; set; }
}

public class LowStockViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int ReorderLevel { get; set; }
}

public class CustomerReportViewModel
{
    public int TotalCustomers { get; set; }
    public List<CustomerOrderViewModel> TopCustomers { get; set; } = new();
    public List<RecentCustomerViewModel> RecentCustomers { get; set; } = new();
}

public class CustomerOrderViewModel
{
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
}

public class RecentCustomerViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime JoinedDate { get; set; }
}