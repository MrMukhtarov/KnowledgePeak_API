using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Setting;

public class SettingIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public SettingIsExistException()
    {
        ErrorMessage = "Status is Exist Database";
    }

    public SettingIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }

}
