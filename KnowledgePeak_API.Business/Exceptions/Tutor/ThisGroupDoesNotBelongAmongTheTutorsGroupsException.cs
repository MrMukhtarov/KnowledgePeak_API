using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Tutor;

internal class ThisGroupDoesNotBelongAmongTheTutorsGroupsException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public ThisGroupDoesNotBelongAmongTheTutorsGroupsException()
    {
        ErrorMessage = "This group does not belong among the tutor's groups";
    }

    public ThisGroupDoesNotBelongAmongTheTutorsGroupsException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
