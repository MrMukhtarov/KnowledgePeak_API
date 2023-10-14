using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

public class TeacherNotEmptyThisDateException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherNotEmptyThisDateException()
    {
        ErrorMessage = "Teacher Does Not Emoty this time";
    }

    public TeacherNotEmptyThisDateException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
