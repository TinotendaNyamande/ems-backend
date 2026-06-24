using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    internal class GetMatrixGetMatrixByIdHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : IRequestHandler<GetMatrixByIdQuery, IEnumerable<GetMatrixDto>>
    {
        public async Task<IEnumerable<GetMatrixDto>> Handle(GetMatrixByIdQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixForOrganisation(request.Id);
        }
    }
}