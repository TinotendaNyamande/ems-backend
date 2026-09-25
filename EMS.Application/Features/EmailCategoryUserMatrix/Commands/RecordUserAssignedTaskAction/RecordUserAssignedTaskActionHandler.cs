using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.RecordUserAssignedTaskAction
{
    internal class RecordUserAssignedTaskActionHandler(IEmailCategoriesUserMatrixRepository emailCategoriesUserMatrix) : ICommandHandler<RecordUserAssignedTaskActionCommand>
    {
        public async Task Handle(RecordUserAssignedTaskActionCommand request, CancellationToken cancellationToken)
        {
            await emailCategoriesUserMatrix.RecordUserAssignedTaskActionAsync(request.MatrixId);
        }
    }
}