using FluentValidation;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetAllUsersAvailableForCategory
{
    public class GetAllUsersAvailableForCategoryValidation:AbstractValidator<GetAllUsersAvailableForCategoryQuery>
    {
        public GetAllUsersAvailableForCategoryValidation()
        {
            RuleFor(x=>x.CategoryId).NotEmpty().WithMessage("Category Id cannot be empty");
        }
    }
}