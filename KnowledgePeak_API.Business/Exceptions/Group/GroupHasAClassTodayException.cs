using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupHasAClassTodayException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupHasAClassTodayException()
    {
        ErrorMessage = "The Group has a classSchedules today";
    }

    public GroupHasAClassTodayException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}

