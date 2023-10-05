using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Token;

public class RefreshTokenExpiresDateException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RefreshTokenExpiresDateException()
    {
        ErrorMessage = "RefreshTOken is timed out";
    }

    public RefreshTokenExpiresDateException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}