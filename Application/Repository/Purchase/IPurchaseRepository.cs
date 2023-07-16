namespace Application;

public interface IPurchaseRepository
{
    Task<ServiceResult<bool>> AddPurchaseAsync(PurchaseModel objPurchase);

    Task<ServiceResult<bool>> DeletePurchase(int id);

    Task<ServiceResult<List<PurchaseModel>>> GetAllCollectionAsync();

    Task<ServiceResult<List<PurchaseModel>>> GetPurchaseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate);

    Task<ServiceResult<List<PurchaseModel>>> GetTodayPurchaseAsync();

    Task<ServiceResult<List<PurchaseModel>>> GetYesterdayPurchaseAsync();
}