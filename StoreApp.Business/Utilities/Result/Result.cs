namespace StoreApp.Business.Utilities.Result;

public class Result 
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public Result(bool success, string message)
    {
        IsSuccess = success;
        Message = message;
    }

    public static Result Success(string message)
    {
        return new Result(true, message);
    }

    public static Result Error(string message)
    {
        return new Result(false, message);
    }
}