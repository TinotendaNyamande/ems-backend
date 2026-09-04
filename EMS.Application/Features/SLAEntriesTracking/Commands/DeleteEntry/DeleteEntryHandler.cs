using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.DeleteEntry
{
    internal class DeleteEntryHandler(ISLATrackingRepository sLATrackingRepository) : IRequestHandler<DeleteEntryCommand>
    {


        public async Task Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {
            await sLATrackingRepository.GetByIdAsync(request.Id);

        }
    }
}