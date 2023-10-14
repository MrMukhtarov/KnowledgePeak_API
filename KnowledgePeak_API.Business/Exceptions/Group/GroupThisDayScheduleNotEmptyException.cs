using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class GroupThisDayScheduleNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public GroupThisDayScheduleNotEmptyException()
    {
        ErrorMessage = "Group This Day Schedule not empty";
    }

    public GroupThisDayScheduleNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
