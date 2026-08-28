using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForAccount
{
    public record GetMatrixForAccountQuery(Guid EmailAccountId) : IRequest<IEnumerable<GetMatrixDto>>
    {
        
    }
}