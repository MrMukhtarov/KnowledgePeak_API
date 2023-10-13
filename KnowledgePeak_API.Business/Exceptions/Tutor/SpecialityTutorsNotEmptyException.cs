using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Tutor;

public class SpecialityTutorsNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SpecialityTutorsNotEmptyException()
    {
        ErrorMessage = "Speciality Tutor is not empty";
    }

    public SpecialityTutorsNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
