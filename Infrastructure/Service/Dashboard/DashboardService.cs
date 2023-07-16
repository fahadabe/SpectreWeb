using System.Net.Http.Json;

namespace Infrastructure;

public class DashboardService : IDashboardService
{
    private readonly HttpClient client;

    public DashboardService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("dashboard");
    }

    public async Task<ServiceResult<PerformanceModel>> GetTodayPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("today");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetYesterdayPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("yesterday");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetThisWeekPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("thisweek");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetThisMonthPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("thismonth");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetYTDPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("ytd");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetAllPerformanceAsync()
    {
        try
        {
            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PerformanceModel>();
                return new ServiceResult<PerformanceModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<PerformanceModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlySaleModel>>> GetSaleChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            string url = onlyCurrentYear ? "salechartdata?onlyCurrentYear=true" : "salechartdata?onlyCurrentYear=false";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<MonthlySaleModel>>();
                return new ServiceResult<List<MonthlySaleModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<MonthlySaleModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlySaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlyExpanseModel>>> GetExpanseChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            string url = onlyCurrentYear ? "expansechartdata?onlyCurrentYear=true" : "expansechartdata";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<MonthlyExpanseModel>>();
                return new ServiceResult<List<MonthlyExpanseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<MonthlyExpanseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlyExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlyPurchaseModel>>> GetPurchaseChartDataAsync(bool onlyCurrentYear)
    {
        try
        {
            string url = onlyCurrentYear ? "purchasechartdata?onlyCurrentYear=true" : "purchasechartdata";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<MonthlyPurchaseModel>>();
                return new ServiceResult<List<MonthlyPurchaseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<MonthlyPurchaseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlyPurchaseModel>> { ErrorMessage = e.Message };
        }
    }
}