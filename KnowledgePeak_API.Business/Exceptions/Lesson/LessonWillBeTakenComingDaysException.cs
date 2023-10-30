using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Lesson;

internal class LessonWillBeTakenComingDaysException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public LessonWillBeTakenComingDaysException()
    {
        ErrorMessage = "The Lesson will be taken in the coming days";
    }

    public LessonWillBeTakenComingDaysException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
