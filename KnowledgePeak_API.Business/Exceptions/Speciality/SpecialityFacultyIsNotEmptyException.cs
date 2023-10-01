using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Speciality;

public class SpecialityFacultyIsNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SpecialityFacultyIsNotEmptyException()
    {
        ErrorMessage = "Speciality Faculty is not empty";
    }

    public SpecialityFacultyIsNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
