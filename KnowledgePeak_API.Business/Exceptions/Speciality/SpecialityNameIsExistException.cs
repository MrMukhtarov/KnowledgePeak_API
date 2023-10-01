using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Speciality;

public class SpecialityNameIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SpecialityNameIsExistException()
    {
        ErrorMessage = "Speciality naem is exist";
    }

    public SpecialityNameIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
