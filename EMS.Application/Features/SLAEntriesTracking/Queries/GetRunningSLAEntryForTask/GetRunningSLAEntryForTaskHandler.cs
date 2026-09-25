using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask
{
    internal class GetRunningSLAEntryForTaskHandler(ISLATrackingRepository sLATrackingRepository) : ICommandHandler<GetRunningSLAEntryForTaskQuery, GetSLATrackingDto>
    {
        public async Task<GetSLATrackingDto> Handle(GetRunningSLAEntryForTaskQuery request, CancellationToken cancellationToken)
        {
            return await sLATrackingRepository.GetCurrentEntryForTaskAsync(request.TaskId);
        }
    }
}