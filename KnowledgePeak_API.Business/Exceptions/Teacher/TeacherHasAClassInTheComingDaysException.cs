using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

public class TeacherHasAClassInTheComingDaysException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherHasAClassInTheComingDaysException()
    {
        ErrorMessage = "The teacher has a class in the coming days";
    }

    public TeacherHasAClassInTheComingDaysException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
