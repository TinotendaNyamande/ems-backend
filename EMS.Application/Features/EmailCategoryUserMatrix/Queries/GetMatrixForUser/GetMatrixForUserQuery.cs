using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForUser
{
    public record GetMatrixForUserQuery(string UserId):ICommand<IEnumerable<GetMatrixDto>>{}
}