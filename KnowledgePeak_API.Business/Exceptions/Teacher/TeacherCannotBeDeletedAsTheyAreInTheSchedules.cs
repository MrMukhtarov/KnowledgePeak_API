using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

public class TeacherCannotBeDeletedAsTheyAreInTheSchedules : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherCannotBeDeletedAsTheyAreInTheSchedules()
    {
        ErrorMessage = "The teacher cannot be deleted as they are in the schedules";
    }

    public TeacherCannotBeDeletedAsTheyAreInTheSchedules(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
