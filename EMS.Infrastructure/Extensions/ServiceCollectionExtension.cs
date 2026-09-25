using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;
using EMS.Infrastructure.RabbitMQServices;
using EMS.Infrastructure.Repository;
using EMS.Infrastructure.Repository.EmailValidation;
using EMS.Infrastructure.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EMS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, IHostEnvironment environment)
        {
            services.Configure<EMS.Application.Settings.EncryptionSettings>(
                config.GetSection("EncryptionSettings")
            );

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ConnStr"));
                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]!))

                };
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleSeeder, RolesSeeder>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailAccountRepository, EmailAccountRepository>();

            services.AddScoped<IEmailProviderValidator, GmailValidator>();
            services.AddScoped<IEmailProviderValidator, OutlookValidator>();
            services.AddScoped<IEmailProviderValidator, Office365Validator>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddDataProtection();
            services.AddScoped<IEncryptionService, AESEncryptionService>();
            services.AddScoped<IEmailCategoryRepository, EmailCategoryRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailTasksRepository, EmailTaskRepository>();
            services.AddScoped<IEmailCategoriesUserMatrixRepository, EmailCategoriesUserMatrixRepository>();
            services.AddScoped<ISLATrackingRepository, SLATrackingRepository>();
            services.AddScoped<ITaskAuditRepository,TaskAuditRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            //services.AddScoped<ITransaction,Transaction>();
            return services;
        }
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitMqOptions>(
                config.GetSection(RabbitMqOptions.SectionName)
            );
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            return services;
        }

    }
}
