using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForAccount
{
    public record GetMatrixForAccountQuery(Guid EmailAccountId) : ICommand<IEnumerable<GetMatrixDto>>
    {
        
    }
}