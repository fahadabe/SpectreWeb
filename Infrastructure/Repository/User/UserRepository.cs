namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection db;

    public UserRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<List<UserModel>>> GetUserListAsync()
    {
        try
        {
            var result = (await db.QueryAsync<UserModel>("SELECT * FROM tblUser")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<UserModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<UserModel>> { ErrorMessage = "No data found" };
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
            string query = "SELECT COUNT(*) FROM tblUser WHERE Username = @a";
            var result = await db.ExecuteScalarAsync<int>(query, new { a = username });
            if (result > 0)
            {
                return new ServiceResult<bool> { Data = true, ErrorMessage = "User with the same name already exist" };
            }
            else
            {
                return new ServiceResult<bool> { Data = false };
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
            string query3 = "INSERT INTO tblUser(Username, Password, CanAddExpanse, CanDeleteExpanse, CanAddSale, CanDeleteSale, CanAddPurchase, CanDeletePurchase, CanViewReport, ManageUsers) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)";
            var result = await db.ExecuteAsync(query3, new
            {
                a = newUser.Username,
                b = newUser.Password,
                c = newUser.CanAddExpanse ? 1 : 0,
                d = newUser.CanDeleteExpanse ? 1 : 0,
                e = newUser.CanAddSale ? 1 : 0,
                f = newUser.CanDeleteSale ? 1 : 0,
                g = newUser.CanAddPurchase ? 1 : 0,
                h = newUser.CanDeletePurchase ? 1 : 0,
                i = newUser.CanViewReport ? 1 : 0,
                j = newUser.ManageUsers ? 1 : 0,
            });
            if (result > 0)
            {
                return new ServiceResult<bool> { Data = true };
            }
            else
            {
                return new ServiceResult<bool> { ErrorMessage = "Failed to insert data into the database." };
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
            string query =
         "UPDATE tblUser SET Username = @a, Password = @b, CanAddExpanse = @c, CanDeleteExpanse = @d, CanAddSale = @e, CanDeleteSale = @f, CanAddPurchase = @g," +
         "CanDeletePurchase = @h, CanViewReport = @i, ManageUsers = @j WHERE UserID = @id";
            var result = await db.ExecuteAsync(query, new
            {
                a = updateUser.Username,
                b = updateUser.Password,
                c = updateUser.CanAddExpanse ? 1 : 0,
                d = updateUser.CanDeleteExpanse ? 1 : 0,
                e = updateUser.CanAddSale ? 1 : 0,
                f = updateUser.CanDeleteSale ? 1 : 0,
                g = updateUser.CanAddPurchase ? 1 : 0,
                h = updateUser.CanDeletePurchase ? 1 : 0,
                i = updateUser.CanViewReport ? 1 : 0,
                j = updateUser.ManageUsers ? 1 : 0,
                id = updateUser.UserID
            });
            if (result > 0)
            {
                return new ServiceResult<bool> { Data = true };
            }
            else
            {
                return new ServiceResult<bool> { ErrorMessage = "Failed to update data in the database." };
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
            string query = "DELETE FROM tblUser WHERE UserID = @a";
            var result = await db.ExecuteAsync(query, new
            {
                a = id
            });
            if (result > 0)
            {
                return new ServiceResult<bool> { Data = true };
            }
            else
            {
                return new ServiceResult<bool> { ErrorMessage = "Failed to delete data from the database." };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }
}