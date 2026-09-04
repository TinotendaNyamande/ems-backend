namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask
{
    using EMS.Application.Dtos.SLATracking;
    using MediatR;
    using System.Collections.Generic;

    public record GetEntriesForTaskQuery (Guid EmailTaskId): IRequest<IEnumerable<SLATrackingEntryDto>>
    {
    }
}