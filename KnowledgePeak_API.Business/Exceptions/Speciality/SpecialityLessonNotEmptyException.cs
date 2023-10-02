using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Speciality;

public class SpecialityLessonNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SpecialityLessonNotEmptyException()
    {
        ErrorMessage = "Speciality Lesson not empty";
    }

    public SpecialityLessonNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
