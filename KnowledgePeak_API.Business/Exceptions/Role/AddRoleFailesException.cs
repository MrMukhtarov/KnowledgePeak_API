using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Role;

public class AddRoleFailesException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public AddRoleFailesException()
    {
        ErrorMessage = "Something wrong to add role";
    }

    public AddRoleFailesException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
