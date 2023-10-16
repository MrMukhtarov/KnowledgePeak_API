using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassSchedules;

public class ClassScheduleNotRemoveDuringLessonException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ClassScheduleNotRemoveDuringLessonException()
    {
        ErrorMessage = "ClassSchedule not delete or soft delete during lesson";
    }

    public ClassScheduleNotRemoveDuringLessonException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
