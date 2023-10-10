using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public static class StudentCheckTImeService
{
    private static Timer _timer;
    private static UserManager<Student> _userManager;

    static StudentCheckTImeService()
    {
        //var currentTime = DateTime.Now;
        //var nextExecutionTIme = currentTime.AddMonths(9);
        //var timeUntilNextExecution = nextExecutionTIme - currentTime;
        //_timer = new Timer(CheckGraduateWrapper, null, timeUntilNextExecution, TimeSpan.FromDays(270));
    }

    public static void Initialize(UserManager<Student> userManager)
    {
        _userManager = userManager;
    }

    public static async Task CheckGraduate()
    {
        var students = await _userManager.Users.ToListAsync();

        foreach (var item in students)
        {
            if (item.EndDate == DateTime.Now.Date || item.EndDate < DateTime.Now.Date)
            {
                item.Status = Status.Graduate;
                await _userManager.UpdateAsync(item);
            }
        }
    }

    //private static async void CheckGraduateWrapper(object state)
    //{
    //    await CheckGraduate();
    //}

    //public static void Dispose()
    //{
    //    _timer.Dispose();
    //}
}
