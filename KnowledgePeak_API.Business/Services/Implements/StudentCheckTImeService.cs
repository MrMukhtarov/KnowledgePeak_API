using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class StudentCheckTImeService
{
    private static UserManager<Student> _userManager;

    public StudentCheckTImeService()
    {
    }

    public void Initialize(UserManager<Student> userManager)
    {
        _userManager = userManager;
    }

    public async Task CheckGraduate()
    {
        var students = await _userManager.Users.ToListAsync();

        foreach (var item in students)
        {
            if (item.EndDate == DateTime.Now || item.EndDate < DateTime.Now)
            {
                item.Status = Status.Graduate;
                await _userManager.UpdateAsync(item);
            }
        }
    }

}
