using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix
{
    internal class DeleteMatrixHandler(IEmailCategoriesUserMatrixRepository matrixRepository):IRequestHandler<DeleteMatrixCommand>
    {
        public async Task Handle(DeleteMatrixCommand command,CancellationToken cancellationToken)
        {
            await matrixRepository.DeleteMatrixAsync(command.Id);
        }
    }
}