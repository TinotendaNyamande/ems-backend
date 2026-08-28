using EMS.Application.Common.Behaviors;
using EMS.Application.Common.Mapping;
using EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateEmailAccountValidator).Assembly);
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateEmailAccountHandler).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(cfg =>
                cfg.AddMaps(typeof(EmailAccountMappingProfile).Assembly));

            return services;
        }
    }
}