using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction
{
    public record RecordUserAssignedTaskActionCommand(Guid MatrixId):IRequest;
}