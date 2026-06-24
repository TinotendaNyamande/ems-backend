using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForOrganisation
{
    public record GetMatrixForOrganisationQuery(Guid OrganisationId) : IRequest<IEnumerable<GetMatrixDto>>
    {
        
    }
}