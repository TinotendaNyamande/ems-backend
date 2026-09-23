using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetAllUsersAvailableForCategory
{
    public record GetAllUsersAvailableForCategoryQuery(Guid? CategoryId):IRequest<IEnumerable<EmailCategoriesUserMatrix>>;
}