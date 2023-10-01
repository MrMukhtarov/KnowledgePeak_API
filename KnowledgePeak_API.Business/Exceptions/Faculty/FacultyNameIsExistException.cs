using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Faculty;

public class FacultyNameIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public FacultyNameIsExistException()
    {
        ErrorMessage = "Faculty name is Exist";
    }

    public FacultyNameIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
