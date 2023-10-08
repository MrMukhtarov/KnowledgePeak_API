using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Speciality;

public class SpecialityTeacherNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SpecialityTeacherNotEmptyException()
    {
        ErrorMessage = "Speciality Teacher not empty";
    }

    public SpecialityTeacherNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
