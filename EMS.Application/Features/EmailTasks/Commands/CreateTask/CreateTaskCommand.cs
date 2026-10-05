using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailTasks.Commands.CreateTask
{
    public record CreateTaskCommand (Guid EmailId,string AssignedToUser):ICommand<EmailTask>{}
}