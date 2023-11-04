using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Room;

public class ThereWillBeALessonInTheRoomItCannotSoftDeletedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ThereWillBeALessonInTheRoomItCannotSoftDeletedException()
    {
        ErrorMessage = "There will be a lesson in the room, it cannot be erased";
    }

    public ThereWillBeALessonInTheRoomItCannotSoftDeletedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
