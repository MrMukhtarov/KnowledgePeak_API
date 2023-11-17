using KnowledgePeak_API.DAL.Repositories.Implements;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgePeak_API.DAL;

public static class RepositoryRegistration
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ISpecialityRepository, SpecialityRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IClassTimeRepository, ClassTimeRepository>();
        services.AddScoped<IClassScheduleRepository, ClassScheduleRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IStudentHistoryRepository, StudentHistoryRepository>();
        services.AddScoped<IContactRepository, ContactRepostory>();
    }
}
