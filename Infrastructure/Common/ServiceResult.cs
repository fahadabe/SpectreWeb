namespace Infrastructure.Common;

public class ServiceResult<T>
{
    public T Data { get; set; }
    public string ErrorMessage { get; set; }
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
}