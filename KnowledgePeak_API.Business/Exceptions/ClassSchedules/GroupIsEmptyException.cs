using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassSchedules;

public class GroupIsEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupIsEmptyException()
    {
        ErrorMessage = "Group IS Empty";
    }

    public GroupIsEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
