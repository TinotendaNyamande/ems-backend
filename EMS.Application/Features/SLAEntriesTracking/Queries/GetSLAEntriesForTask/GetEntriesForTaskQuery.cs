namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask
{
    using EMS.Application.Abstractions;
    using EMS.Application.Dtos.SLATracking;
    using System.Collections.Generic;

    public record GetEntriesForTaskQuery (Guid EmailTaskId): ICommand<IEnumerable<GetSLATrackingDto>>
    {
    }
}