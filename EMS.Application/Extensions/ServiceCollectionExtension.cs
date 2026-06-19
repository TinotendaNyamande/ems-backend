using EMS.Application.Common.Behaviors;
using EMS.Application.Common.Mapping;
using EMS.Application.Features.Organisations.Commands.ChangeOwner;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ChangeOwnerValidator).Assembly);
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateOrganisationHandler).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(cfg =>
                cfg.AddMaps(typeof(OrganisationMappingProfile).Assembly));

            return services;
        }
    }
}