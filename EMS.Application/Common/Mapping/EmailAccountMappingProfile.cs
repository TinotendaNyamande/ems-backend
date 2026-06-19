using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.EmailAccounts.Commands.ChangeClientSecret;
using EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword;
using EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class EmailAccountMappingProfile:Profile
    {
        public EmailAccountMappingProfile()
        {
            CreateMap<CreateEmailAccountCommand,EmailAccount>();
            CreateMap<EmailAccount, EmailAccountDto>()
                .ForMember(dest => dest.ParentOrganisation, opt =>
                opt.MapFrom(src => new OrganisationDto
                {
                    Id = src.Organisation.Id,
                    Name = src.Organisation.Name,
                    OwnerId = src.Organisation.OwnerId,
                }));
            CreateMap<ChangeEmailAccountPasswordCommand, ChangeEmailPasswordDto>();
            CreateMap<ChangeClientSecretCommand, ChangeClientSecretDto>();
            CreateMap<EmailAccount,ValidationAndTestEmailAccountDto>();
        }
    }
}
