using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask
{
    public record GetRunningSLAEntryForTaskQuery(Guid TaskId):IQuery<GetSLATrackingDto?>;
}