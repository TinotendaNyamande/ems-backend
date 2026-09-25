using EMS.Application.Abstractions;
using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditById
{
    internal class GetAuditByIdHandler(ITaskAuditRepository taskAuditRepository) : ICommandHandler<GetAuditByIdQuery, GetTaskAuditTrailDto>
    {
        public async Task<GetTaskAuditTrailDto> Handle(GetAuditByIdQuery request, CancellationToken cancellationToken)
        {
            return await taskAuditRepository.GetAuditTrailEntryById(request.Id);
        }
    }
}