using KnowledgePeak_API.Business.Services.Implements;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgePeak_API.Business;

public static class ServiceRegistration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IUniversityService, UniversityService>();
    }
}
