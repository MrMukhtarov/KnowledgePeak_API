using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class LoginFailedException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public LoginFailedException()
    {
        ErrorMessage = typeof(T).Name + " Login failed some reason";
    }

    public LoginFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
