using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Tutor;

public class TutorSpecialityIsNotBeNullException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TutorSpecialityIsNotBeNullException()
    {
        ErrorMessage = "Tutor speciality not be null";
    }

    public TutorSpecialityIsNotBeNullException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
