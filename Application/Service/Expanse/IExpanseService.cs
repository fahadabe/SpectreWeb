namespace Application;

public interface IExpanseService
{
    Task<ServiceResult<bool>> AddExpanseAsync(ExpanseModel objExpanse);

    Task<ServiceResult<bool>> DeleteExpanse(int id);

    Task<ServiceResult<List<ExpanseModel>>> GetAllCollectionAsync();

    Task<ServiceResult<List<ExpanseModel>>> GetExpanseBetweenTwoDatesAsync(DateTime? fromDate, DateTime? toDate);

    Task<ServiceResult<List<ExpanseModel>>> GetTodayExpanseAsync();

    Task<ServiceResult<List<ExpanseModel>>> GetYesterdayExpanseAsync();
}