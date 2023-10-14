using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

public class TeacherDoesNotTeachThisLessonException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherDoesNotTeachThisLessonException()
    {
        ErrorMessage = "Teacher Does Not Teach this lesson";
    }

    public TeacherDoesNotTeachThisLessonException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
