using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetAllUsersAvailableForCategory
{
    internal class GetAllUsersAvailableForCategoryHandler(IEmailCategoriesUserMatrixRepository emailCategoriesUserMatrixRepository) : IRequestHandler<GetAllUsersAvailableForCategoryQuery, IEnumerable<EmailCategoriesUserMatrix>>
    {
        public async Task<IEnumerable<EmailCategoriesUserMatrix>> Handle(GetAllUsersAvailableForCategoryQuery request, CancellationToken cancellationToken)
        {
            return await emailCategoriesUserMatrixRepository.GetAllAvailableForCategoryAsync(request.CategoryId);
        }
    }
}