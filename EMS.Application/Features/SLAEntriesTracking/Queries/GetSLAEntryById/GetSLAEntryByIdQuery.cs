using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetSLAEntryById
{
    public record GetSLAEntryByIdQuery(Guid Id) : IQuery<GetSLATrackingDto>
    {
    }


}