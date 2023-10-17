using KnowledgePeak_API.Business.ExternalServices.Implements;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Implements;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgePeak_API.Business;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IUniversityService, UniversityService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<ISpecialityService, SpecialityService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IDirectorService, DirectorService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITutorService, TutorService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IClassTimeService, CLassTimeService>();
        services.AddScoped<IClassScheduleService, ClassScheduleService>();
        services.AddScoped<IGradeService, GradeService>();
    }
}
