namespace Domain.Common;

public static class ExtensionMethods
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
    {
        return new ObservableCollection<T>(source);
    }

    public static bool ReturnSuccess<TI>(this TI input) where TI : class
    {
        return input != null;
    }
}