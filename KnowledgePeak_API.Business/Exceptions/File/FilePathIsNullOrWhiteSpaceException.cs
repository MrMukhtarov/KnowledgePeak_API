using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.File;

public class FilePathIsNullOrWhiteSpaceException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public FilePathIsNullOrWhiteSpaceException()
    {
        ErrorMessage = "File Path is Null or White Space";
    }

    public FilePathIsNullOrWhiteSpaceException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
