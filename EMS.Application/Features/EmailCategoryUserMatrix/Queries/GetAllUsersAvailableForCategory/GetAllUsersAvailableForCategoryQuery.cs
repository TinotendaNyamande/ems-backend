using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetAllUsersAvailableForCategory
{
    public record GetAllUsersAvailableForCategoryQuery(Guid? CategoryId):ICommand<IEnumerable<EmailCategoriesUserMatrix>>;
}