using BusinessLogic.Interfaces;
using BusinessLogic.Mapping;
using BusinessLogic.Services;
using DataAccess.Data;
using DataAccess.Infrastructure;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Middleware;

namespace WeatherForecastWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Forecast", Version = "v1" });
            });

            builder.Services.AddDbContext<WeatherForecastDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherForecast")));

            builder.Services.AddHostedService<WeatherDataCleanupService>();
            builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("https://localhost:7096")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            var app = builder.Build();

            app.UseMiddleware<CustomExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}