using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class UserExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UserExistException()
    {
        ErrorMessage = "User is Exist";
    }

    public UserExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
