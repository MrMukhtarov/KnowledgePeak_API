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
        services.AddScoped<IFileService, FileService>();
    }
}
