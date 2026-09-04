namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntryById
{
    using AutoMapper;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Interfaces;
    using MediatR;

    public class GetEntryByIdHandler(ISLATrackingRepository slaTrackingRepository,IMapper mapper) : IRequestHandler<GetEntryByIdQuery, SLATrackingEntryDto>
    {

        public async Task<SLATrackingEntryDto> Handle(GetEntryByIdQuery request, CancellationToken cancellationToken)
        {
            var entry = await slaTrackingRepository.GetByIdAsync(request.Id);
            return mapper.Map<SLATrackingEntryDto>(entry);
        }
    }
}