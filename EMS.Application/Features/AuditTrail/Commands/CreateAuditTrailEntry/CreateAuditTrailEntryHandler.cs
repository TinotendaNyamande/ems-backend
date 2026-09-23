using AutoMapper;
using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry
{
    public class CreateAuditTrailEntryHandler(ITaskAuditRepository taskAuditRepository,IMapper mapper) : IRequestHandler<CreateAuditTrailEntryCommand,GetTaskAuditTrailDto>
    {
        public async Task<GetTaskAuditTrailDto> Handle(CreateAuditTrailEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = mapper.Map<TaskAuditTrail>(request);
            var createdEntry = await taskAuditRepository.CreateTaskAuditTrailAsync(entry);
            return await taskAuditRepository.GetAuditTrailEntryById(createdEntry.Id);
        }
    }
}