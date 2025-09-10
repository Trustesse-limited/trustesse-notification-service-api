namespace Ivoluntia.BackgroudServices.Common;

public class ApiResponse<T>
{
    public bool Successs { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }


    public static ApiResponse<T> Failure(int statusCode, string message)
    {
        return new ApiResponse<T>
        {
            Successs = false,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static ApiResponse<T> Success(string message, T data)
    {
        return new ApiResponse<T>
        {
            Successs = true,
            StatusCode = StatusCodes.Status200OK,
            Message = message,
            Data = data
        };
    }
}
