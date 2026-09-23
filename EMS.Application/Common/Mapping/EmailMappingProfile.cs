using AutoMapper;
using EMS.Application.Dtos.Emails;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.Emails.Commands.CreateEmail;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class EmailMappingProfile : Profile
    {
        public EmailMappingProfile()
        {
            CreateMap<CreateEmailCommand,Email>();
            CreateMap<IncomingEmailDto,Email>();
        }
    }
}