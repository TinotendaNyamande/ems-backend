namespace EMS.Application.Common.Mapping
{
    using AutoMapper;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateEntry;
    using EMS.Domain.Models;

    public class SLATrackingMappingProfile : Profile
    {
        public SLATrackingMappingProfile()
        {
            CreateMap<SLATracking, SLATrackingEntryDto>();
            CreateMap<UpdateEntryCommand,UpdateSLAEntryDto>();
        }
    }
}