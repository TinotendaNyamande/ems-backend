using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask
{
    internal class GetEntriesForTaskHandler(ISLATrackingRepository slaTrackingRepository) : IQueryHandler<GetEntriesForTaskQuery, IEnumerable<GetSLATrackingDto>>
    {
        public async Task<IEnumerable<GetSLATrackingDto>> Handle(GetEntriesForTaskQuery request, CancellationToken cancellationToken)
        {
            return await slaTrackingRepository.GetByEmailTaskIdAsync(request.EmailTaskId);
        }
    }
}