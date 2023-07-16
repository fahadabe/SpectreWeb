namespace Application;

public interface ILoginService
{
    Task<ServiceResult<UserModel>> LoginAsync(UserModel login);
}