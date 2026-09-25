using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    internal class GetMatrixGetMatrixByIdHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : ICommandHandler<GetMatrixByIdQuery, GetMatrixDto>
    {
        public async Task<GetMatrixDto>  Handle(GetMatrixByIdQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixByIdAsync(request.Id);
        }
    }
}