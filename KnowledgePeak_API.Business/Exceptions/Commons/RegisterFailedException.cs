using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class RegisterFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RegisterFailedException()
    {
        ErrorMessage = "Register failed some reason";
    }

    public RegisterFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
