using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.File;

public class FileTypeInvalidExveption : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public FileTypeInvalidExveption()
    {
        ErrorMessage = "File Type is Invalid";
    }

    public FileTypeInvalidExveption(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
