namespace Application;

public interface IExpanseRepository
{
    Task<ServiceResult<bool>> AddExpanseAsync(ExpanseModel objExpanse);

    Task<ServiceResult<List<ExpanseModel>>> GetAllCollectionAsync();

    Task<ServiceResult<List<ExpanseModel>>> GetTodayExpanseAsync();

    Task<ServiceResult<List<ExpanseModel>>> GetYesterdayExpanseAsync();

    Task<ServiceResult<List<ExpanseModel>>> GetExpanseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate);

    Task<ServiceResult<bool>> DeleteExpanse(int id);
}