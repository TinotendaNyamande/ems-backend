using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetSLAEntryById
{
    public record GetSLAEntryByIdQuery(Guid Id) : ICommand<GetSLATrackingDto>
    {
    }


}