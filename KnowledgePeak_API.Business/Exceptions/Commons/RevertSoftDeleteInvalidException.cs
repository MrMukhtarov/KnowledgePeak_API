using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class RevertSoftDeleteInvalidException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RevertSoftDeleteInvalidException()
    {
        ErrorMessage = typeof(T).Name + " RevertSoft Delete Inavlid";
    }

    public RevertSoftDeleteInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
