namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntryById
{
    using AutoMapper;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Interfaces;
    using MediatR;

    public class GetEntryByIdHandler(ISLATrackingRepository slaTrackingRepository,IMapper mapper) : IRequestHandler<GetEntryByIdQuery, GetSLATrackingDto>
    {

        public async Task<GetSLATrackingDto> Handle(GetEntryByIdQuery request, CancellationToken cancellationToken)
        {
            return await slaTrackingRepository.GetByIdAsync(request.Id);
        }
    }
}