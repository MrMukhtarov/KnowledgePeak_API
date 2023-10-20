using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

internal class EmailNotConfirmedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public EmailNotConfirmedException()
    {
        ErrorMessage = "Email not confiremd";
    }

    public EmailNotConfirmedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
