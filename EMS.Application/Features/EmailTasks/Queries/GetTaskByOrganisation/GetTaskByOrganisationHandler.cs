using EMS.Application.Dtos.Tasks;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByOrganisation
{
    public class GetTaskByOrganisationHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<GetTaskByOrganisationQuery, IEnumerable<GetTasksDto>>
    {
        public async Task<IEnumerable<GetTasksDto>> Handle(GetTaskByOrganisationQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTasksForOrganisation(request.OrganisationId, request.Status);
        }
    }
}
