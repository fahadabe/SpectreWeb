namespace Application;

public interface ILoginRepository
{
    Task<ServiceResult<UserModel>> LoginAsync(UserModel login);
}