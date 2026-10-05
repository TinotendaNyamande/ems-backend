using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    internal class UpdateSLAEntryHandler(ISLATrackingRepository sLATrackingRepository) : ICommandHandler<UpdateSLAEntryCommand>
    {
        public async Task Handle(UpdateSLAEntryCommand request, CancellationToken cancellationToken)
        {
            await sLATrackingRepository.StopTimerAsync(request.Id);

        }
    }
}