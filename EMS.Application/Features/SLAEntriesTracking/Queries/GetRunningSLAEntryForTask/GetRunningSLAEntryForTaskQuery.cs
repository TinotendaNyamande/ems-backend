using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask
{
    public record GetRunningSLAEntryForTaskQuery(Guid TaskId):ICommand<GetSLATrackingDto>;
}