using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Commons;

public class UserProfileUpdateException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public UserProfileUpdateException()
    {
        ErrorMessage = "An error occurred while editing the profile.";
    }

    public UserProfileUpdateException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
