using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Room;

public class RoomNameIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RoomNameIsExistException()
    {
        ErrorMessage = "Thi room name has already exist";
    }

    public RoomNameIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
