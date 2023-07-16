using System.Net.Http.Json;

namespace Infrastructure;

public class LoginService : ILoginService
{
    private readonly HttpClient client;

    public LoginService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("login");
    }

    public async Task<ServiceResult<UserModel>> LoginAsync(UserModel login)
    {
        try
        {
            var response = await client.PostAsJsonAsync("login", login);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserModel>();
                return new ServiceResult<UserModel> { Data = result };
            }
            else
            {
                var errorMessage = $"An error occurred while logging in: {response.ReasonPhrase}";
                return new ServiceResult<UserModel> { ErrorMessage = errorMessage };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<UserModel> { ErrorMessage = e.Message };
        }
    }
}