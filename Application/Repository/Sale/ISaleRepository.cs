namespace Application;

public interface ISaleRepository
{
    Task<ServiceResult<bool>> AddSaleAsync(SaleModel objSale);

    Task<ServiceResult<bool>> DeleteSale(int id);

    Task<ServiceResult<List<SaleModel>>> GetAllCollectionAsync();

    Task<ServiceResult<List<SaleModel>>> GetSaleBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate);

    Task<ServiceResult<List<SaleModel>>> GetTodaySaleAsync();

    Task<ServiceResult<List<SaleModel>>> GetYesterdaySaleAsync();
}