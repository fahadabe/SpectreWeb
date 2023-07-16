namespace Infrastructure;

public class ExpanseRepository : IExpanseRepository
{
    private readonly IDbConnection db;

    public ExpanseRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<bool>> AddExpanseAsync(ExpanseModel objExpanse)
    {
        try
        {
            string query = $"INSERT INTO tblExpanse(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
            var result = await db.ExecuteAsync(query, new
            {
                a = objExpanse.Date.ToString("yyyy-MM-dd"),
                b = objExpanse.Description,
                c = objExpanse.Amount,
                d = objExpanse.AddedBy
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

    public async Task<ServiceResult<List<ExpanseModel>>> GetAllCollectionAsync()
    {
        try
        {
            var result = (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse ORDER BY ExpanseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', 'localtime') ORDER BY ExpanseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY ExpanseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<ExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<ExpanseModel>>> GetExpanseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var result = (await db.QueryAsync<ExpanseModel>($"SELECT * FROM tblExpanse WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY ExpanseID DESC")).ToList();

            if (result.Count > 0)
            {
                return new ServiceResult<List<ExpanseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<ExpanseModel>> { ErrorMessage = "No data found" };
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
            string query = $"DELETE FROM tblExpanse WHERE ExpanseID = @a";
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
                return new ServiceResult<bool> { ErrorMessage = "Failed to delete data." };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<bool> { ErrorMessage = e.Message };
        }
    }

    //public async Task<(bool success, string message)> AddExpanseAsync(ExpanseModel objExpanse)
    //{
    //    try
    //    {
    //        string query = $"INSERT INTO tblExpanse(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
    //        var result = await db.ExecuteAsync(query, new
    //        {
    //            a = objExpanse.Date.ToString("yyyy-MM-dd"),
    //            b = objExpanse.Description,
    //            c = objExpanse.Amount,
    //            d = objExpanse.AddedBy
    //        });
    //        if (result > 0)
    //        {
    //            return (true, "");
    //        }
    //        else
    //        {
    //            return (false, "Failed to insert data into the database.");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        return (false, e.Message);
    //    }
    //}

    //public async Task<(List<ExpanseModel> collection, string message)> GetAllCollectionAsync()
    //{
    //    try
    //    {
    //        var result =  (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse ORDER BY ExpanseID DESC")).ToList();
    //        if (result.Count > 0)
    //        {
    //            return (collection: result, "");
    //        }
    //        else
    //        {
    //            return (collection: new List<ExpanseModel>(), "No data found");
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        return (collection: new List<ExpanseModel>(), e.Message);
    //    }
    //}

    //public async Task<(List<ExpanseModel> collection , string message)> GetTodayExpanseAsync()
    //{
    //    try
    //    {
    //        var result =  (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', 'localtime') ORDER BY ExpanseID DESC")).ToList();
    //        if (result.Count > 0)
    //        {
    //            return (collection: result, "");
    //        }
    //        else
    //        {
    //            return (collection: new List<ExpanseModel>(), "No data found");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        return (collection: new List<ExpanseModel>(), e.Message);
    //    }
    //}

    //public async Task<(List<ExpanseModel> collection, string message)> GetYesterdayExpanseAsync()
    //{
    //    try
    //    {
    //        var result = (await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY ExpanseID DESC")).ToList();
    //        if (result.Count > 0)
    //        {
    //            return (collection: result, "");
    //        }
    //        else
    //        {
    //            return (collection: new List<ExpanseModel>(), "No data found");
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        return (collection: new List<ExpanseModel>(), e.Message);
    //    }
    //}

    //public async Task<(List<ExpanseModel> collection, string message)> GetExpanseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    //{
    //    try
    //    {
    //        var result = (await db.QueryAsync<ExpanseModel>($"SELECT * FROM tblExpanse WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY ExpanseID DESC")).ToList();

    //        if (result.Count > 0)
    //        {
    //            return (collection: result, "");
    //        }
    //        else
    //        {
    //            return (collection: new List<ExpanseModel>(), "No data found");
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        return (collection: new List<ExpanseModel>(), e.Message);
    //    }
    //}

    //public async Task<(bool success, string message)> DeleteExpanse(int id)
    //{
    //    try
    //    {
    //        string query = $"DELETE FROM tblExpanse WHERE ExpanseID = @a";
    //        var result = await db.ExecuteAsync(query, new
    //        {
    //            a = id
    //        });
    //        if (result > 0)
    //        {
    //            return (true, "");
    //        }
    //        else
    //        {
    //            return (false, "Failed to delete data.");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        return (false, e.Message);
    //    }
    //}
}