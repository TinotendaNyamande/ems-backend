using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    internal class UpdateSLAEntryHandler(ISLATrackingRepository sLATrackingRepository, IMapper mapper) : ICommandHandler<UpdateSLAEntryCommand>
    {
        public async Task Handle(UpdateSLAEntryCommand request, CancellationToken cancellationToken)
        {
            var entryDto = mapper.Map<UpdateSLAEntryDto>(request);
            await sLATrackingRepository.StopTimerAsync(request.Id, entryDto);

        }
    }
}