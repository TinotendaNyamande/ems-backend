using EMS.API.Middleware;
using EMS.Application.Settings;

namespace EMS.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:3000", "http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });
            services.Configure<EncryptionSettings>(
                configuration.GetSection("EncryptionSettings")
            );

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddScoped<ExceptionHandlingMiddleware>();
            return services;
        }
    }

}