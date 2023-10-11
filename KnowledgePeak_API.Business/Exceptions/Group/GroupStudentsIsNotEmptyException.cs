using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupStudentsIsNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupStudentsIsNotEmptyException()
    {
        ErrorMessage = "Group Students has not null";
    }

    public GroupStudentsIsNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
