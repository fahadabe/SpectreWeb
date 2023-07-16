namespace Infrastructure;

public class DashboardRepository : IDashboardRepository
{
    private readonly IDbConnection db;

    public DashboardRepository(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<ServiceResult<PerformanceModel>> GetTodayPerformance()
    {
        try
        {
            PerformanceModel todayPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date = DATE('now', 'localtime')";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date = DATE('now', 'localtime')";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date = DATE('now', 'localtime')";

            todayPerformance.Identifier = "Today";
            todayPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            todayPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            todayPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (todayPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = todayPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetYesterdayPerformance()
    {
        try
        {
            PerformanceModel yesterdayPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date = DATE('now', '-1 days', 'localtime')";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date = DATE('now', '-1 days', 'localtime')";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date = DATE('now', '-1 days', 'localtime')";

            yesterdayPerformance.Identifier = "Yesterday";
            yesterdayPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            yesterdayPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            yesterdayPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (yesterdayPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = yesterdayPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetThisWeekPerformance()
    {
        try
        {
            PerformanceModel thisWeekPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";

            thisWeekPerformance.Identifier = "This Week";
            thisWeekPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            thisWeekPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            thisWeekPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (thisWeekPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = thisWeekPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetThisMonthPerformance()
    {
        try
        {
            PerformanceModel thisMonthPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date BETWEEN DATE('now','start of month') AND DATE('now','localtime')";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date BETWEEN DATE('now','start of month') AND DATE('now','localtime')";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date BETWEEN DATE('now','start of month') AND DATE('now','localtime')";

            thisMonthPerformance.Identifier = "This Month";
            thisMonthPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            thisMonthPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            thisMonthPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (thisMonthPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = thisMonthPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetYTDPerformance()
    {
        try
        {
            PerformanceModel ytdPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";

            ytdPerformance.Identifier = "Year to date";
            ytdPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            ytdPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            ytdPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (ytdPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = ytdPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<PerformanceModel>> GetAllPerformance()
    {
        try
        {
            PerformanceModel allPerformance = new();

            string saleQuery = $"SELECT SUM(Amount) FROM tblSale";
            string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase";
            string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse";

            allPerformance.Identifier = "All";
            allPerformance.Sale = await db.ExecuteScalarAsync<double>(saleQuery);
            allPerformance.Purchase = await db.ExecuteScalarAsync<double>(purchaseQuery);
            allPerformance.Expanse = await db.ExecuteScalarAsync<double>(expanseQuery);

            if (allPerformance is not null)
            {
                return new ServiceResult<PerformanceModel> { Data = allPerformance };
            }
            else
            {
                return new ServiceResult<PerformanceModel> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<PerformanceModel> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlySaleModel>>> GetSaleChartData(bool onlyCurrentYear)
    {
        try
        {
            List<MonthlySaleModel> result = new();
            string query = "SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblSale GROUP BY STRFTIME('%Y-%m', Date)";
            var data = await db.QueryAsync(query);

            foreach (var row in data)
            {
                if (!string.IsNullOrWhiteSpace(row.Month))
                {
                    string[] tokens = row.Month.Split('-');
                    string year = tokens[0];
                    string month = tokens[1];
                    if (onlyCurrentYear)
                    {
                        if (year == DateTime.Now.Year.ToString())
                        {
                            result.Add(new MonthlySaleModel
                            {
                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                Sale = Convert.ToInt32(row.Value)
                            });
                        }
                    }
                    else
                    {
                        result.Add(new MonthlySaleModel
                        {
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                            Sale = Convert.ToInt32(row.Value)
                        });
                    }
                }
            }

            if (result.Count > 0)
            {
                return new ServiceResult<List<MonthlySaleModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<MonthlySaleModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlySaleModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlyExpanseModel>>> GetExpanseChartData(bool onlyCurrentYear)
    {
        try
        {
            List<MonthlyExpanseModel> result = new();
            string query = "SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblExpanse GROUP BY STRFTIME('%Y-%m', Date)";
            var data = await db.QueryAsync(query);

            foreach (var row in data)
            {
                if (!string.IsNullOrWhiteSpace(row.Month))
                {
                    string[] tokens = row.Month.Split('-');
                    string year = tokens[0];
                    string month = tokens[1];
                    if (onlyCurrentYear)
                    {
                        if (year == DateTime.Now.Year.ToString())
                        {
                            result.Add(new MonthlyExpanseModel
                            {
                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                Expanse = Convert.ToInt32(row.Value)
                            });
                        }
                    }
                    else
                    {
                        result.Add(new MonthlyExpanseModel
                        {
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                            Expanse = Convert.ToInt32(row.Value)
                        });
                    }
                }
            }

            if (result.Count > 0)
            {
                return new ServiceResult<List<MonthlyExpanseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<MonthlyExpanseModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlyExpanseModel>> { ErrorMessage = e.Message };
        }
    }

    public async Task<ServiceResult<List<MonthlyPurchaseModel>>> GetPurchaseChartData(bool onlyCurrentYear)
    {
        try
        {
            List<MonthlyPurchaseModel> result = new();
            string query = "SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblPurchase GROUP BY STRFTIME('%Y-%m', Date)";
            var data = await db.QueryAsync(query);

            foreach (var row in data)
            {
                if (!string.IsNullOrWhiteSpace(row.Month))
                {
                    string[] tokens = row.Month.Split('-');
                    string year = tokens[0];
                    string month = tokens[1];
                    if (onlyCurrentYear)
                    {
                        if (year == DateTime.Now.Year.ToString())
                        {
                            result.Add(new MonthlyPurchaseModel
                            {
                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                Purchase = Convert.ToInt32(row.Value)
                            });
                        }
                    }
                    else
                    {
                        result.Add(new MonthlyPurchaseModel
                        {
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                            Purchase = Convert.ToInt32(row.Value)
                        });
                    }
                }
            }

            if (result.Count > 0)
            {
                return new ServiceResult<List<MonthlyPurchaseModel>> { Data = result };
            }
            else
            {
                return new ServiceResult<List<MonthlyPurchaseModel>> { ErrorMessage = "No data found" };
            }
        }
        catch (Exception e)
        {
            return new ServiceResult<List<MonthlyPurchaseModel>> { ErrorMessage = e.Message };
        }
    }
}