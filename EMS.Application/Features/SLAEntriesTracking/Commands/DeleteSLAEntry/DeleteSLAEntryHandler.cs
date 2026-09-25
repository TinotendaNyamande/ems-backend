using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteSLAEntry
{
    internal class DeleteEntryHandler(ISLATrackingRepository sLATrackingRepository) : ICommandHandler<DeleteSLAEntryCommand>
    {


        public async Task Handle(DeleteSLAEntryCommand request, CancellationToken cancellationToken)
        {
            await sLATrackingRepository.GetByIdAsync(request.Id);

        }
    }
}