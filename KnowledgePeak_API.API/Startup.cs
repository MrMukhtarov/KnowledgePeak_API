using Hangfire;
using KnowledgePeak_API.Business.Services.Implements;
using Microsoft.AspNetCore.Identity;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(_configuration.GetConnectionString("MSSQL")));

    }
    StudentCheckTImeService stu = new StudentCheckTImeService();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate(() => stu.CheckGraduate(), Cron.MinuteInterval(1));

        app.UseHangfireServer();
    }
}
