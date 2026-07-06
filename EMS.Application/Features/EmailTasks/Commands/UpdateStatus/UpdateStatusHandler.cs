using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.EmailTasks.Commands.UpdateStatus
{
    internal class UpdateStatusHandler(IEmailTasksRepository tasksRepository, 
    IRolesRepository rolesRepository,
    ILogger<UpdateStatusHandler> logger
    ) : IRequestHandler<UpdateStatusCommand>
    {
        public async Task Handle(UpdateStatusCommand command, CancellationToken token)
        {
            logger.LogInformation("Updating status for task {TaskId} to {NewStatus}", command.Id, command.NewStatus);
            if (command.NewStatus == TaskStatusList.Escalated &&
                command.OrganisationId.HasValue)
            {
                logger.LogInformation("Task {TaskId} is being escalated. Attempting to reassign to a suitable user.", command.Id);
                var organisationId = command.OrganisationId.Value;

                foreach (var role in new[] { "Supervisor", "Manager", "Owner" })
                {
                    var users = await rolesRepository.GetOrganisationUsersByRoleAsync(role, organisationId);

                    var user = users.FirstOrDefault();

                    if (user != null)
                    {
                        logger.LogInformation("Found user {UserId} with role {Role} for escalation of task {TaskId}. Reassigning task.", user.Id, role, command.Id);
                        await tasksRepository.ReassignTaskAsync(command.Id, user.Id);
                        logger.LogInformation("Task {TaskId} successfully reassigned to user {UserId}.", command.Id, user.Id);
                        break;
                    }

                    if (role == "Owner")
                    {
                        logger.LogError("No suitable user found for escalation of task {TaskId}.", command.Id);
                        throw new InvalidOperationException(
                            "No suitable user found for escalation.");
                    }
                }
            }

            await tasksRepository.ChangeTaskStatusAsync(
                command.Id,     
                command.NewStatus,
                command.AdditionalInformation);
        }
    }
}