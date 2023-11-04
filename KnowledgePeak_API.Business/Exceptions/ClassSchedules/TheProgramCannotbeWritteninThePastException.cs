using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.ClassSchedules;

public class TheProgramCannotbeWritteninThePastException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TheProgramCannotbeWritteninThePastException()
    {
        ErrorMessage = "The program cannot be written in the past";
    }

    public TheProgramCannotbeWritteninThePastException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
