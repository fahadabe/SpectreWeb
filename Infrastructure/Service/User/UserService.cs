using System.Net.Http.Json;

namespace Infrastructure;

public class UserService : IUserService
{
    private readonly HttpClient client;

    public UserService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("user");
    }

    public async Task<ServiceResult<List<UserModel>>> GetUserListAsync()
    {
        try
        {
            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                return new ServiceResult<List<UserModel>> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while retrieving data: {response.ReasonPhrase}";
                return new ServiceResult<List<UserModel>> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<UserModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> CheckIfUserExist(string username)
    {
        try
        {
            var response = await client.GetAsync($"exist/{username}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new ServiceResult<bool> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while checking if user exists: {response.ReasonPhrase}";
                return new ServiceResult<bool> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> UpdateUser(UserModel updateUser)
    {
        try
        {
            var response = await client.PutAsJsonAsync("", updateUser);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new ServiceResult<bool> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while updating data: {response.ReasonPhrase}";
                return new ServiceResult<bool> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<bool>> AddNewUser(UserModel newUser)
    {
        try
        {
            var response = await client.PostAsJsonAsync("", newUser);

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

    public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
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
        catch (Exception ex)
        {
            return new ServiceResult<bool> { ErrorMessage = ex.Message };
        }
    }
}