namespace StoreApp.Business.Utilities.Result;
public class DataResult<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public T Data { get; }

    public DataResult(T data, bool success, string message)
    {
        Data = data;
        IsSuccess = success;
        Message = message;
    }

    public static DataResult<T> Success(T data, string message)
    {
        return new DataResult<T>(data, true, message);
    }

    public static DataResult<T> Error(T data, string message)
    {
        return new DataResult<T>(data, false, message);
    }
}
