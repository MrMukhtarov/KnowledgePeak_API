using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class UserNotFoundException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UserNotFoundException()
    {
        ErrorMessage = typeof(T).Name + " Not Found";  
    }

    public UserNotFoundException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
