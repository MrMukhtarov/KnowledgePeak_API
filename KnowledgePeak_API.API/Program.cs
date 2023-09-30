using KnowledgePeak_API.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using KnowledgePeak_API.DAL;
using KnowledgePeak_API.Business.Profiles;
using KnowledgePeak_API.API.Helpers;

namespace KnowledgePeak_API.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });

            builder.Services.AddRepository();

            builder.Services.AddAutoMapper(typeof(UniversityMappingProfile).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCustomExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}