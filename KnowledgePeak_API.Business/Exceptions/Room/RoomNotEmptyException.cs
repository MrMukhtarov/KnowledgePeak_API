using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Room;

public class RoomNotEmptyException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public RoomNotEmptyException()
    {
        ErrorMessage = "This room does not emoty in this time";
    }

    public RoomNotEmptyException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
