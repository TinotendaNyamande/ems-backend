using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForOrganisation
{
    internal class GetMatrixForOrganisationHandler(IEmailCategoriesUserMatrixRepository matrixRepository) : IRequestHandler<GetMatrixForOrganisationQuery, IEnumerable<GetMatrixDto>>
    {
        public async Task<IEnumerable<GetMatrixDto>> Handle(GetMatrixForOrganisationQuery request,CancellationToken cancellationToken)
        {
            return await matrixRepository.GetMatrixForOrganisation(request.OrganisationId);
        }
    }
}