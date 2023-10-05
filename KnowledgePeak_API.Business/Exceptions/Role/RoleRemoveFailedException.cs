using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Role;

public class RoleRemoveFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RoleRemoveFailedException()
    {
        ErrorMessage = "Some problems emerged while deleting the role.";
    }

    public RoleRemoveFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
