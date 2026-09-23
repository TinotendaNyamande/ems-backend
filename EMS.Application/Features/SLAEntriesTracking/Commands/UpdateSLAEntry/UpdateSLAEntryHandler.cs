using AutoMapper;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateSLAEntry
{
    internal class UpdateSLAEntryHandler(ISLATrackingRepository sLATrackingRepository, IMapper mapper) : IRequestHandler<UpdateSLAEntryCommand>
    {
        public async Task Handle(UpdateSLAEntryCommand request, CancellationToken cancellationToken)
        {
            var entryDto = mapper.Map<UpdateSLAEntryDto>(request);
            await sLATrackingRepository.StopTimerAsync(request.Id, entryDto);

        }
    }
}