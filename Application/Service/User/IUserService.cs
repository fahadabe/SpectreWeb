namespace Application;

public interface IUserService
{
    Task<ServiceResult<bool>> AddNewUser(UserModel newUser);

    Task<ServiceResult<bool>> CheckIfUserExist(string username);

    Task<ServiceResult<bool>> DeleteUserAsync(int id);

    Task<ServiceResult<List<UserModel>>> GetUserListAsync();

    Task<ServiceResult<bool>> UpdateUser(UserModel updateUser);
}