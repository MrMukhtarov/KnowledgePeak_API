using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class ConfirmEmailInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ConfirmEmailInvalidException()
    {
        ErrorMessage = "Email Confirmed Invalid for some reason";
    }

    public ConfirmEmailInvalidException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
