using System.Net.Http.Json;

namespace Infrastructure;

public class SaleService : ISaleService
{
    private readonly HttpClient client;

    public SaleService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("sale");
    }

    public async Task<ServiceResult<bool>> AddSaleAsync(SaleModel objSale)
    {
        try
        {
            var response = await client.PostAsJsonAsync("", objSale);

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

    public async Task<ServiceResult<List<SaleModel>>> GetAllCollectionAsync()
    {
        try
        {
            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleModel>>();
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<SaleModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<SaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<SaleModel>>> GetTodaySaleAsync()
    {
        try
        {
            var response = await client.GetAsync("today");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleModel>>();
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<SaleModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<SaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<SaleModel>>> GetYesterdaySaleAsync()
    {
        try
        {
            var response = await client.GetAsync("yesterday");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleModel>>();
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<SaleModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<SaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<SaleModel>>> GetSaleBetweenTwoDatesAsync(DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            var response = await client.GetAsync($"between/{fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<SaleModel>>();
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<SaleModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<SaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> DeleteSale(int id)
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