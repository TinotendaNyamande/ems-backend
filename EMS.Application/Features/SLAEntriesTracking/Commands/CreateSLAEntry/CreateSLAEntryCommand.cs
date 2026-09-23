using EMS.Application.Dtos.SLATracking;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry
{
    public record CreateSLAEntryCommand (Guid EmailTaskId,string Comments ): IRequest<SLATracking>{}
}