

namespace Application;

public interface IDashboardService
{
    Task<ServiceResult<PerformanceModel>> GetAllPerformanceAsync();

    Task<ServiceResult<List<MonthlyExpanseModel>>> GetExpanseChartDataAsync(bool onlyCurrentYear);

    Task<ServiceResult<List<MonthlyPurchaseModel>>> GetPurchaseChartDataAsync(bool onlyCurrentYear);

    Task<ServiceResult<List<MonthlySaleModel>>> GetSaleChartDataAsync(bool onlyCurrentYear);

    Task<ServiceResult<PerformanceModel>> GetThisMonthPerformanceAsync();

    Task<ServiceResult<PerformanceModel>> GetThisWeekPerformanceAsync();

    Task<ServiceResult<PerformanceModel>> GetTodayPerformanceAsync();

    Task<ServiceResult<PerformanceModel>> GetYesterdayPerformanceAsync();

    Task<ServiceResult<PerformanceModel>> GetYTDPerformanceAsync();
}