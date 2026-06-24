using EMS.Application.Dtos.Tasks;
using EMS.Domain.Enums;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByOrganisation
{
    public class GetTaskByOrganisationQuery(Guid organisationId, TaskStatusList? status = null) : IRequest<IEnumerable<GetTasksDto>>
    {
        public Guid OrganisationId { get; init; } = organisationId;
        public TaskStatusList? Status { get; init; } = status;
    }
}
