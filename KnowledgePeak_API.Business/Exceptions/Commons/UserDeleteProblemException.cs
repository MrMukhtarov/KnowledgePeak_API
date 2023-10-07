using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class UserDeleteProblemException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UserDeleteProblemException()
    {
        ErrorMessage = "Delete time have error occured";
    }

    public UserDeleteProblemException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
