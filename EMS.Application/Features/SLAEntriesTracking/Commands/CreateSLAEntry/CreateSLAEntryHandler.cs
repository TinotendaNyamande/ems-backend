using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.SLAEntriesTracking.Commands.CreateSLAEntry
{
    internal class CreateSLAEntryHandler(ISLATrackingRepository sLATrackingRepository,IMapper mapper) : ICommandHandler<CreateSLAEntryCommand, SLATracking>
    {
        public async Task<SLATracking> Handle(CreateSLAEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = mapper.Map<SLATracking>(request);
            var savedEntry = await sLATrackingRepository.CreateSLAEntryAsync(entry);
            return savedEntry;
        }
    }
}