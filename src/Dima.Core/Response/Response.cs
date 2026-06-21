using System.Text.Json.Serialization;

namespace Dima.Core.Response;

public class Response<T>
{
    [JsonConstructor]
    public Response()
    {
        IsSuccess = true;
    }
    protected Response(T data)
    {
        Data = data;
        IsSuccess = true;
    }

    protected  Response(Error error)
    {
        Error = error;
    }
    public T? Data { get; set; }
    public Error? Error { get; set; }
    [JsonIgnore]
    public bool IsSuccess { get;set; }

    public static Response<T> Failure(Error error) => new (error);
    public static Response<T> Success(T value) => new(value);
    public static Response<T> Success() => new();
    
    public static implicit operator Response<T>(Error error)
        => new (error);

    public static implicit operator Response<T>(T value)
        => new(value);
}