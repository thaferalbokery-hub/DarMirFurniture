using DarMirFurniture.ViewModels;

namespace DarMirFurniture.Services;

public interface IReportService
{
    Task<DashboardViewModel> GetDashboardAsync();
    Task<SalesReportViewModel> GetSalesReportAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<ProductReportViewModel> GetProductReportAsync();
    Task<CustomerReportViewModel> GetCustomerReportAsync();
}