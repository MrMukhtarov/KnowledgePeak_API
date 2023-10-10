using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class SIgnOutInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SIgnOutInvalidException()
    {
        ErrorMessage = "An error occurred while SignOut the profile.";
    }

    public SIgnOutInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
