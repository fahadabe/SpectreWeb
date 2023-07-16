namespace Infrastructure;

public class SaleRepository : ISaleRepository
{
    private readonly IDbConnection db;

    public SaleRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<bool>> AddSaleAsync(SaleModel objSale)
    {
        try
        {
            string query = $"INSERT INTO tblSale(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
            var result = await db.ExecuteAsync(query, new
            {
                a = objSale.Date.ToString("yyyy-MM-dd"),
                b = objSale.Description,
                c = objSale.Amount,
                d = objSale.AddedBy
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

    public async Task<ServiceResult<List<SaleModel>>> GetAllCollectionAsync()
    {
        try
        {
            var result = (await db.QueryAsync<SaleModel>("Select * From tblSale ORDER BY SaleID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<SaleModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<SaleModel>("Select * From tblSale WHERE Date = DATE('now', 'localtime') ORDER BY SaleID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<SaleModel>> { ErrorMessage = "No data found" };
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
            var result = (await db.QueryAsync<SaleModel>("Select * From tblSale WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY SaleID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<SaleModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<SaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<SaleModel>>> GetSaleBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            var result = (await db.QueryAsync<SaleModel>($"SELECT * FROM tblSale WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY SaleID DESC")).ToList();
            if (result.Count > 0)
            {
                return new ServiceResult<List<SaleModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<SaleModel>> { ErrorMessage = "No data found" };
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
            string query = $"DELETE FROM tblSale WHERE SaleID = @id";
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