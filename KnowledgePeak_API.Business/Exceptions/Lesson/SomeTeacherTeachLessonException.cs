using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Lesson;

public class SomeTeacherTeachLessonException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public SomeTeacherTeachLessonException()
    {
        ErrorMessage = "Some teacher teach this lesson";
    }

    public SomeTeacherTeachLessonException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
