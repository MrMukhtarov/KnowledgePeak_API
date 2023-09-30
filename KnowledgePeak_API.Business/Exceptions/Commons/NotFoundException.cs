using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class NotFoundException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public NotFoundException()
    {
        ErrorMessage = typeof(T).Name + " Not Found";
    }

    public NotFoundException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
