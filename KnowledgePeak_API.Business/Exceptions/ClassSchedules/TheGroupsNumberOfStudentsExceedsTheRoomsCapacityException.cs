using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassSchedules;

public class TheGroupsNumberOfStudentsExceedsTheRoomsCapacityException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TheGroupsNumberOfStudentsExceedsTheRoomsCapacityException()
    {
        ErrorMessage = "The group's number of students exceeds the room's capacity.";
    }

    public TheGroupsNumberOfStudentsExceedsTheRoomsCapacityException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
