using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForUser
{
    internal class GetMatrixForUserHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : IRequestHandler<GetMatrixForUserQuery, IEnumerable<GetMatrixDto>>
    {
        public async Task<IEnumerable<GetMatrixDto>> Handle(GetMatrixForUserQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixForUserAsync(request.UserId);
        }
    }
}