using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Lesson;

public class LessonIsIntheGradeException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public LessonIsIntheGradeException()
    {
        ErrorMessage = "Lesson is in the some grade";
    }

    public LessonIsIntheGradeException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
