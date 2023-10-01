using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class IdIsNegativeException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public IdIsNegativeException()
    {
        ErrorMessage = typeof(T).Name + " Id is less than 0 or equal";
    }

    public IdIsNegativeException(string? message) : base(message)
    {
        ErrorMessage = message;
    }

}
