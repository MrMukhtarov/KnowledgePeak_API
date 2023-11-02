using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassSchedules;

internal class TheLessonCannotBeDeletedAsItBelongsToAPastTimeException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TheLessonCannotBeDeletedAsItBelongsToAPastTimeException()
    {
        ErrorMessage = "The ClassSchedules cannot be Updated as it belongs to a past time.";
    }

    public TheLessonCannotBeDeletedAsItBelongsToAPastTimeException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
