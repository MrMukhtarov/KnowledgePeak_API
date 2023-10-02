﻿using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Exceptions.Lesson;

public class LessonNameIsExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public LessonNameIsExistException()
    {
        ErrorMessage = "Lesson name is Exist";
    }

    public LessonNameIsExistException(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
