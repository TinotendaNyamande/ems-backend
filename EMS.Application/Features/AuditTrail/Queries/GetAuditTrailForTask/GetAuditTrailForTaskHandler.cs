using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditTrailForTask
{
    internal class GetAuditTrailForTaskHandler(ITaskAuditRepository taskAuditRepository) : IRequestHandler<GetAuditTrailForTaskQuery, IEnumerable<GetTaskAuditTrailDto>>
    {
        public async Task<IEnumerable<GetTaskAuditTrailDto>> Handle(GetAuditTrailForTaskQuery request, CancellationToken cancellationToken)
        {
            return await taskAuditRepository.GetTaskAuditTrail(request.TaskId);
        }
    }
}