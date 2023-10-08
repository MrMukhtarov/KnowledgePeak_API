using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Faculty;

public class FacultyTeachersNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public FacultyTeachersNotEmptyException()
    {
        ErrorMessage = "Faculty Teachers not empty";
    }

    public FacultyTeachersNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
