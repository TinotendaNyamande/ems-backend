using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    internal class GetMatrixGetMatrixByIdHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : IQueryHandler<GetMatrixByIdQuery, GetMatrixDto>
    {
        public async Task<GetMatrixDto>  Handle(GetMatrixByIdQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixByIdAsync(request.Id)?? throw new ResourceNotFoundException("Email category user matrix", request.Id);
        }
    }
}