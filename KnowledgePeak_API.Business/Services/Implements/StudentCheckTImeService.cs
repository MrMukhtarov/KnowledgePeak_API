using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class StudentCheckTImeService
{
    private Timer _timer;
    readonly UserManager<Student> _userManager;
    public StudentCheckTImeService(UserManager<Student> userManager)
    {
        var currentTime = DateTime.Now;
        var nextExecutionTIme = currentTime.AddMonths(9);
        var timeUntilNextExecution = nextExecutionTIme - currentTime;
        _timer.Change(timeUntilNextExecution, TimeSpan.FromDays(270));
        _userManager = userManager;
    }
    public async Task CheckGraduate()
    {
        var students = await _userManager.Users.ToListAsync();

        foreach (var item in students)
        {
            if (item.EndDate == DateTime.Now.Date)
            {
                item.Status = Status.Graduate;
            }
        }
    }
    public void Dispose()
    {
        _timer.Dispose();
    }
}
