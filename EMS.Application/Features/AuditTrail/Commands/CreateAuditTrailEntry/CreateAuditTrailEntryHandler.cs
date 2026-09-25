using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Features.AuditTrail.Queries.GetAuditById;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry
{
    public class CreateAuditTrailEntryHandler(ITaskAuditRepository taskAuditRepository) : ICommandHandler<CreateAuditTrailEntryCommand, TaskAuditTrail>
    {
        public async Task<TaskAuditTrail> Handle(CreateAuditTrailEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = new TaskAuditTrail(request.Comments,request.UserId,request.TaskId);
            return await taskAuditRepository.CreateTaskAuditTrailAsync(entry);
        }
    }
}