using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Director;

public class ThereIsaDirectorInTheSystemException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ThereIsaDirectorInTheSystemException()
    {
        ErrorMessage = "There is a director in the system.";
    }

    public ThereIsaDirectorInTheSystemException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
