using KnowledgePeak_API.Business.Exceptions.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgePeak_API.Business.Exceptions.Group;

public class TheGroupHasSchedulesTheyCannotDetele : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public TheGroupHasSchedulesTheyCannotDetele()
    {
        ErrorMessage = "The classroom has schedules, they cannot be deleted";
    }

    public TheGroupHasSchedulesTheyCannotDetele(string? message) : base(message)
    {
        ErrorMessage = message;
    }
}
