using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Lesson;

internal class LessonIsOnTheClassScheduleException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public LessonIsOnTheClassScheduleException()
    {
        ErrorMessage = "This class is on the class schedule";
    }

    public LessonIsOnTheClassScheduleException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
