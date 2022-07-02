using System.Text.Json.Serialization;

namespace WebAPI.Service.Dtos;

public class ResponseDto<T>
{
    [JsonIgnore]
    public int StatusCode { get; set; }
    public string Status { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }

    public static ResponseDto<T> Success(int statusCode,T data)
    {
        return new ResponseDto<T>()
        {
            Data = data,
            Status = "success",
            StatusCode = statusCode
        };
    }

    public static ResponseDto<T> Success(int statusCode)
    {
        return new ResponseDto<T>()
        {
            Status = "success",
            StatusCode = statusCode,
            Data = default
        };
    }
    
    public static ResponseDto<T> Fail(int statusCode,string error)
    {
        return new ResponseDto<T>()
        {
            Error = error,
            Status = "fail",
            StatusCode = statusCode
        };
    }

}