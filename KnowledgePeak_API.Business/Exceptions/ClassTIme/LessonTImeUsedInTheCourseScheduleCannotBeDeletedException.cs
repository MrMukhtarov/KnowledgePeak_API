using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassTIme;

public class LessonTImeUsedInTheCourseScheduleCannotBeDeletedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public LessonTImeUsedInTheCourseScheduleCannotBeDeletedException()
    {
        ErrorMessage = "Lesson time used in the course schedule cannot be deleted";
    }

    public LessonTImeUsedInTheCourseScheduleCannotBeDeletedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
