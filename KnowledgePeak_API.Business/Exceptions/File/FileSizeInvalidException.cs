using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.File;

public class FileSizeInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public FileSizeInvalidException()
    {
        ErrorMessage = "FIle Size is Invalid";
    }

    public FileSizeInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
