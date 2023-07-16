using System.Net.Http.Json;

namespace Infrastructure;

public class PurchaseService : IPurchaseService
{
    private readonly HttpClient client;

    public PurchaseService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("purchase");
    }

    public async Task<ServiceResult<bool>> AddPurchaseAsync(PurchaseModel objPurchase)
    {
        try
        {
            var response = await client.PostAsJsonAsync("", objPurchase);

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

    public async Task<ServiceResult<List<PurchaseModel>>> GetAllCollectionAsync()
    {
        try
        {
            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<PurchaseModel>>();
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<PurchaseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<PurchaseModel>>> GetTodayPurchaseAsync()
    {
        try
        {
            var response = await client.GetAsync("today");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<PurchaseModel>>();
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<PurchaseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<PurchaseModel>>> GetYesterdayPurchaseAsync()
    {
        try
        {
            var response = await client.GetAsync("yesterday");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<PurchaseModel>>();
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<PurchaseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<PurchaseModel>>> GetPurchaseBetweenTwoDatesAsync(DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            var response = await client.GetAsync($"between/{fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<PurchaseModel>>();
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<PurchaseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeletePurchase(int id)
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