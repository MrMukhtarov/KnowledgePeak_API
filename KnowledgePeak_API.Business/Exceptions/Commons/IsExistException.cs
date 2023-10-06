using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class IsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public IsExistException()
    {
        ErrorMessage = "Item is exist";
    }

    public IsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
