namespace Infrastructure;

public class LoginRepository : ILoginRepository
{
    private readonly IDbConnection db;

    public LoginRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<UserModel>> LoginAsync(UserModel login)
    {
        try
        {
            string query = $"SELECT * from tblUser WHERE Username = @username AND Password = @password";
            var user = await db.QueryFirstOrDefaultAsync<UserModel>(query, new
            {
                username = login.Username,
                password = login.Password
            });

            if (user is not null)
            {
                return new ServiceResult<UserModel> { Data = user };
            }
            else
            {
                return new ServiceResult<UserModel> { ErrorMessage = "No such user found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<UserModel> { ErrorMessage = e.Message };
        }
    }
}