namespace Application;

public interface IDashboardRepository
{
    Task<ServiceResult<PerformanceModel>> GetAllPerformance();

    Task<ServiceResult<List<MonthlyExpanseModel>>> GetExpanseChartData(bool onlyCurrentYear);

    Task<ServiceResult<List<MonthlyPurchaseModel>>> GetPurchaseChartData(bool onlyCurrentYear);

    Task<ServiceResult<List<MonthlySaleModel>>> GetSaleChartData(bool onlyCurrentYear);

    Task<ServiceResult<PerformanceModel>> GetThisMonthPerformance();

    Task<ServiceResult<PerformanceModel>> GetThisWeekPerformance();

    Task<ServiceResult<PerformanceModel>> GetTodayPerformance();

    Task<ServiceResult<PerformanceModel>> GetYesterdayPerformance();

    Task<ServiceResult<PerformanceModel>> GetYTDPerformance();
}