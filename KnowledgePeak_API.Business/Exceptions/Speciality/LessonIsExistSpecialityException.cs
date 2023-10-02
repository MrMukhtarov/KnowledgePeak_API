using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Speciality;

public class LessonIsExistSpecialityException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public LessonIsExistSpecialityException()
    {
        ErrorMessage = "Thi lesson has already exist";
    }

    public LessonIsExistSpecialityException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}

