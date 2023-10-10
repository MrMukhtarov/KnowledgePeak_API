using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class SoftDeleteInvalidException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SoftDeleteInvalidException()
    {
        ErrorMessage = typeof(T).Name + " Soft Delete Inavlid";
    }

    public SoftDeleteInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
