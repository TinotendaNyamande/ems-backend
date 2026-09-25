using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry
{
    public record CreateSLAEntryCommand (Guid EmailTaskId,string UserId,string Comments ): ICommand<SLATracking>{}
}