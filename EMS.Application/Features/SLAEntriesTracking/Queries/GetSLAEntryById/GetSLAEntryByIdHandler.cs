namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetSLAEntryById
{
    using EMS.Application.Abstractions;
    using EMS.Application.Dtos.SLATracking;
    using EMS.Application.Interfaces;
    using EMS.Domain.Exceptions;

    public class GetSLAEntryByIdHandler(ISLATrackingRepository slaTrackingRepository) : IQueryHandler<GetSLAEntryByIdQuery, GetSLATrackingDto>
    {

        public async Task<GetSLATrackingDto> Handle(GetSLAEntryByIdQuery request, CancellationToken cancellationToken)
        {
            return await slaTrackingRepository.GetByIdAsync(request.Id)?? throw new ResourceNotFoundException("SLA entry", request.Id);
        }
    }
}