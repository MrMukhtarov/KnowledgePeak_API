using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Grade;

public class ItsBennFiftyMinutesSinceTheGradeWasGivenException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ItsBennFiftyMinutesSinceTheGradeWasGivenException()
    {
        ErrorMessage = "\r\nIt's been 15 minutes since the grade was given.";
    }

    public ItsBennFiftyMinutesSinceTheGradeWasGivenException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
