using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.University;

public class UniversityIsExistException : Exception, IBaseException
{

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UniversityIsExistException()
    {
        ErrorMessage = "University is Exist database";
    }

    public UniversityIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }

}
