using AutoMapper;
using EMS.Application.Dtos.SLATracking;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.UpdateEntry
{
    internal class UpdateEntryHandler(ISLATrackingRepository sLATrackingRepository, IMapper mapper) : IRequestHandler<UpdateEntryCommand>
    {
        public async Task Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
        {
            var entryDto = mapper.Map<UpdateSLAEntryDto>(request);
            await sLATrackingRepository.UpdateAsync(request.Id, entryDto);

        }
    }
}