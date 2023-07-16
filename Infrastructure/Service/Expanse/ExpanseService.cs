using System.Net.Http.Json;

namespace Infrastructure;

public class ExpanseService : IExpanseService
{
    private readonly HttpClient client;

    public ExpanseService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("expanse");
    }

    public async Task<ServiceResult<bool>> AddExpanseAsync(ExpanseModel objExpanse)
    {
        try
        {
            var response = await client.PostAsJsonAsync("", objExpanse);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new ServiceResult<bool> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while adding data: {response.ReasonPhrase}";
                return new ServiceResult<bool> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<ExpanseModel>>> GetAllCollectionAsync()
    {
        try
        {
            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ExpanseModel>>();
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<ExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<ExpanseModel>>> GetTodayExpanseAsync()
    {
        try
        {
            var response = await client.GetAsync("today");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ExpanseModel>>();
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<ExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<ExpanseModel>>> GetYesterdayExpanseAsync()
    {
        try
        {
            var response = await client.GetAsync("yesterday");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ExpanseModel>>();
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<ExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<ExpanseModel>>> GetExpanseBetweenTwoDatesAsync(DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            var response = await client.GetAsync($"between/{fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ExpanseModel>>();
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<ExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteExpanse(int id)
    {
        try
        {
            var response = await client.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new ServiceResult<bool> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while deleting data: {response.ReasonPhrase}";
                return new ServiceResult<bool> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }
}