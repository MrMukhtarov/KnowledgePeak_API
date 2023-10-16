using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupHasClassSchedulesInTheComingDaysException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupHasClassSchedulesInTheComingDaysException()
    {
        ErrorMessage = "The Group has a classSchedules in the coming days";
    }

    public GroupHasClassSchedulesInTheComingDaysException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
