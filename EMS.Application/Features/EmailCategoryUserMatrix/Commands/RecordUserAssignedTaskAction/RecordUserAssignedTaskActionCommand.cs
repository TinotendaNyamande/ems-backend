using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction
{
    public record RecordUserAssignedTaskActionCommand(Guid MatrixId):ICommand;
}