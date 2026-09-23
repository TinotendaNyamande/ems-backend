using AutoMapper;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask
{
    internal class GetEntriesForTaskHandler(ISLATrackingRepository slaTrackingRepository, IMapper mapper) : IRequestHandler<GetEntriesForTaskQuery, IEnumerable<GetSLATrackingDto>>
    {
        public async Task<IEnumerable<GetSLATrackingDto>> Handle(GetEntriesForTaskQuery request, CancellationToken cancellationToken)
        {
            return await slaTrackingRepository.GetByEmailTaskIdAsync(request.EmailTaskId);
        }
    }
}