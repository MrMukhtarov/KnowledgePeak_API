using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Role;

public class RoleUpdateFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RoleUpdateFailedException()
    {
        ErrorMessage = "Some problems emerged while Update the role.";
    }

    public RoleUpdateFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
