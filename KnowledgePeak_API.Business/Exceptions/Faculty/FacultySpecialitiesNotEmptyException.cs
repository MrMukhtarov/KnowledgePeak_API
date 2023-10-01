using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Faculty;

public class FacultySpecialitiesNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public FacultySpecialitiesNotEmptyException()
    {
        ErrorMessage = "Faculty Specialities not empty";
    }

    public FacultySpecialitiesNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
