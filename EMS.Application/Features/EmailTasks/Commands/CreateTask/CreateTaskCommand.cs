using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.CreateTask
{
    public record CreateTaskCommand (Guid EmailId,string AssignedToUser):IRequest<EmailTask>{}
}