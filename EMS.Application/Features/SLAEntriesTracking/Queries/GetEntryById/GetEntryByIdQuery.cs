using EMS.Application.Dtos.SLATracking;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntryById
{
    public record GetEntryByIdQuery(Guid Id) : IRequest<SLATrackingEntryDto>
    {
    }


}