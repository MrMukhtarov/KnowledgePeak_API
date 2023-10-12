using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Tutor;

public class TutorGroupsNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TutorGroupsNotEmptyException()
    {
        ErrorMessage = "Tutor Groups not be null";
    }

    public TutorGroupsNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
