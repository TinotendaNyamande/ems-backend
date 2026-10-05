using EMS.Application.Abstractions;
using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditById
{
    internal class GetAuditByIdHandler(ITaskAuditRepository taskAuditRepository) : IQueryHandler<GetAuditByIdQuery, GetTaskAuditTrailDto>
    {
        public async Task<GetTaskAuditTrailDto> Handle(GetAuditByIdQuery request, CancellationToken cancellationToken)
        {
            return await taskAuditRepository.GetAuditTrailEntryById(request.Id) ?? throw new ResourceNotFoundException("Audit trail entry", request.Id);
            
        }
    }
}