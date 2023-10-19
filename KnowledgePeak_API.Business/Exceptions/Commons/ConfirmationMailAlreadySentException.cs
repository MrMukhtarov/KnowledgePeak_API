using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class ConfirmationMailAlreadySentException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ConfirmationMailAlreadySentException()
    {
        ErrorMessage = "return Content(\"\", \"Mail already sent. if you dont see in your inbox. Please check spams. You can send Confirmation mail after ";
    }

    public ConfirmationMailAlreadySentException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
