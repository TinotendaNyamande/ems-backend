using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForUser
{
    public record GetMatrixForUserQuery(string UserId):IQuery<IEnumerable<GetMatrixDto>>{}
}