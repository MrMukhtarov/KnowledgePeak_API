using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

internal class YourAccountHasBeenSuspendedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public YourAccountHasBeenSuspendedException()
    {
        ErrorMessage = "Your account has been suspended";
    }

    public YourAccountHasBeenSuspendedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
