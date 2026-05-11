using AutoMapper;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret;
using EMS.Application.Features.EmailConfigs.Commands.ChangeEmailPassword;
using EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class EmailConfigMappingProfile:Profile
    {
        public EmailConfigMappingProfile()
        {
            CreateMap<CreateEmailConfigCommand,MailBoxConfig>();
            CreateMap<MailBoxConfig, EmailConfigDto>()
                .ForMember(dest => dest.ParentOrganisation, opt =>
                opt.MapFrom(src => new OrganisationDto
                {
                    Id = src.Organisation.Id,
                    Name = src.Organisation.Name,
                    OwnerId = src.Organisation.OwnerId,
                }));
            CreateMap<ChangeEmailPasswordCommand, ChangeEmailPasswordDto>();
            CreateMap<ChangeClientSecretCommand, ChangeClientSecretDto>();
        }
    }
}
