using System.Text.Json.Serialization;

namespace Shared.Commons;
public class Result<T>
{
    private Result(int code, string? message)
    {
        StatusCode = code;
        IsSuccess = true;
        Message = message;
    }

    private Result(T data, int code, string? message)
    {
        Data = data;
        IsSuccess = true;
        StatusCode = code;
        Message = message;
    }

    private Result(ErrorRecord error, string? message)
    {
        IsSuccess = false;
        Error = error;
        StatusCode = error.Code;
        Message = message;
    }

    public bool IsSuccess { get; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; } = default;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorRecord? Error { get; }
    public int StatusCode { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    public static Result<T> Success(int code = 200, string? message = null) => new(code, message);
    public static Result<T> Created() => new(201, "Item has been created");
    public static Result<T> FromResult(T value, int code = 200, string? message = null) => new(value, code, message);
    public static Result<T> Failure(ErrorRecord error, string? message = null) => new(error, message);
}
