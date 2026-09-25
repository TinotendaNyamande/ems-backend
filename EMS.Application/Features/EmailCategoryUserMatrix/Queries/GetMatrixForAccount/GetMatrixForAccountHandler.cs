using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForAccount
{
    internal class GetMatrixForAccountHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : ICommandHandler<GetMatrixForAccountQuery, IEnumerable<GetMatrixDto>>
    {
        public async Task<IEnumerable<GetMatrixDto>> Handle(GetMatrixForAccountQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixForEmailAccountAsync(request.EmailAccountId);
        }
    }
}