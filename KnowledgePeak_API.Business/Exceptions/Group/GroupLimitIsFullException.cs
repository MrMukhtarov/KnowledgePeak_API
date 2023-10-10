using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupLimitIsFullException : Exception, IBaseException
{ 
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupLimitIsFullException()
    {
        ErrorMessage = "Group Limit is Full";
    }

    public GroupLimitIsFullException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
