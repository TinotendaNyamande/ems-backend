namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetSLAEntryById
{
    using AutoMapper;
    using EMS.Application.Abstractions;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Interfaces;
    using MediatR;

    public class GetSLAEntryByIdHandler(ISLATrackingRepository slaTrackingRepository,IMapper mapper) : ICommandHandler<GetSLAEntryByIdQuery, GetSLATrackingDto>
    {

        public async Task<GetSLATrackingDto> Handle(GetSLAEntryByIdQuery request, CancellationToken cancellationToken)
        {
            return await slaTrackingRepository.GetByIdAsync(request.Id);
        }
    }
}