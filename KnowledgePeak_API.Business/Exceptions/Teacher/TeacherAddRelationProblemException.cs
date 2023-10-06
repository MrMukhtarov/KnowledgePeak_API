using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Teacher;

public class TeacherAddRelationProblemException<T> : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TeacherAddRelationProblemException()
    {
        ErrorMessage = typeof(T).Name + " Add is failed some reason"; 
    }

    public TeacherAddRelationProblemException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
