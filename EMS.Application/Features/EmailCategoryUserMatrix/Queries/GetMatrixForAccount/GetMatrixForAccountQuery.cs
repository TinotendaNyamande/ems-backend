using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForAccount
{
    public record GetMatrixForAccountQuery(Guid EmailAccountId) : IQuery<IEnumerable<GetMatrixDto>>
    {
        
    }
}