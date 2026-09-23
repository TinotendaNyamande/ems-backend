namespace EMS.Application.Common.Mapping
{
    using AutoMapper;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry;
    using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry;
    using EMS.Domain.Models;

    public class SLATrackingMappingProfile : Profile
    {
        public SLATrackingMappingProfile()
        {
            CreateMap<SLATracking, SLATrackingEntryDto>();
            CreateMap<UpdateSLAEntryCommand,UpdateSLAEntryDto>();
            CreateMap<CreateSLAEntryCommand,SLATracking>();
        }
    }
}