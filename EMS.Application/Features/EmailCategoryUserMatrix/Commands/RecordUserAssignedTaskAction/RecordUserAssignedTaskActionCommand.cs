using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction
{
    public record RecordUserAssignedTaskActionCommand(Guid MatrixId):ICommand;
}