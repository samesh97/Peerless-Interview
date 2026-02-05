using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PeerlessInterview.src.Api.Response;

public class CommonResponse
{
    public string? ErrorMessage { get; set; }
    public Object? Data { get; set; }

    public static CommonResponse Success<T>(T data)
    {
        return new CommonResponse
        {
            Data = data
        };
    }

     public static CommonResponse Error(string errorMessage)
    {
        return new CommonResponse
        {
            ErrorMessage = errorMessage
        };
    }
}