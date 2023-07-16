namespace Infrastructure;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly IDbConnection db;

    public PurchaseRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<bool>> AddPurchaseAsync(PurchaseModel objPurchase)
    {
        try
        {
            string query = $"INSERT INTO tblPurchase(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
            var result = await db.ExecuteAsync(query, new
            {
                a = objPurchase.Date.ToString("yyyy-MM-dd"),
                b = objPurchase.Description,
                c = objPurchase.Amount,
                d = objPurchase.AddedBy
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

    public async Task<ServiceResult<List<PurchaseModel>>> GetAllCollectionAsync()
    {
        try
        {
            var result = (await db.QueryAsync<PurchaseModel>("Select * From tblPurchase ORDER BY PurchaseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<PurchaseModel>("Select * From tblPurchase WHERE Date = DATE('now', 'localtime') ORDER BY PurchaseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<PurchaseModel>("Select * From tblPurchase WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY PurchaseID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<PurchaseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<PurchaseModel>>> GetPurchaseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var result = (await db.QueryAsync<PurchaseModel>($"SELECT * FROM tblPurchase WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY PurchaseID DESC")).ToList();

            if (result.Count > 0)
            {
                return new ServiceResult<List<PurchaseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<PurchaseModel>> { ErrorMessage = "No data found" };
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
            string query = $"DELETE FROM tblPurchase WHERE PurchaseID = @id";
            var result = await db.ExecuteAsync(query, new
            {
                id = id
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