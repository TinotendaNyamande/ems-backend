using FluentValidation;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById
{
    public class GetEmailCategoryByIdValidation:AbstractValidator<GetEmailCategoryByIdQuery>
    {
        public GetEmailCategoryByIdValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Email Category Id cannot be empty");
        }
    }
}