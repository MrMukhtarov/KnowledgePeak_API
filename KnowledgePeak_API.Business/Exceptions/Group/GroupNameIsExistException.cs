using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupNameIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupNameIsExistException()
    {
        ErrorMessage = "Group Name is Exist";
    }

    public GroupNameIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
