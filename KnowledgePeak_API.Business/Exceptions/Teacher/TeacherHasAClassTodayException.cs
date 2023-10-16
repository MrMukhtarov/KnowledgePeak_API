using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

internal class TeacherHasAClassTodayException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherHasAClassTodayException()
    {
        ErrorMessage = "The teacher has a class today";
    }

    public TeacherHasAClassTodayException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
