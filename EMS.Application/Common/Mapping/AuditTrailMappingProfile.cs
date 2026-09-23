using AutoMapper;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class AuditTrailMappingProfile : Profile
    {
        public AuditTrailMappingProfile()
        {
            CreateMap<CreateAuditTrailEntryCommand,TaskAuditTrail>();
        }
    }
}