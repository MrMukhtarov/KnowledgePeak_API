using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class IsExistIdException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public IsExistIdException()
    {
        ErrorMessage = typeof(T).Name + " Is Exist";
    }

    public IsExistIdException(string? message) : base(message)
    {
        ErrorMessage = message;
    }


}
