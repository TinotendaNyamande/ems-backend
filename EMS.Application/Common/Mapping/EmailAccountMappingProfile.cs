using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
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
            CreateMap<EmailAccount, EmailAccountDto>();
            CreateMap<ChangeEmailAccountPasswordCommand, ChangeEmailPasswordDto>();
            CreateMap<ChangeClientSecretCommand, ChangeClientSecretDto>();
            CreateMap<EmailAccount,ValidationAndTestEmailAccountDto>();
        }
    }
}
