using AutoMapper;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask
{
    internal class GetEntriesForTaskHandler(ISLATrackingRepository slaTrackingRepository, IMapper mapper) : IRequestHandler<GetEntriesForTaskQuery, IEnumerable<SLATrackingEntryDto>>
    {
        public async Task<IEnumerable<SLATrackingEntryDto>> Handle(GetEntriesForTaskQuery request, CancellationToken cancellationToken)
        {
            var entries = await slaTrackingRepository.GetByEmailTaskIdAsync(request.EmailTaskId);
            return mapper.Map<IEnumerable<SLATrackingEntryDto>>(entries);
        }
    }
}