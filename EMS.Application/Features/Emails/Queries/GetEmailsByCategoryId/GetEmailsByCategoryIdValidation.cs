using FluentValidation;

namespace EMS.Application.Features.Emails.Queries.GetEmailsByCategoryId
{
    public class GetEmailsByCategoryIdValidation : AbstractValidator<GetEmailsByCategoryIdQuery>
    {
        public GetEmailsByCategoryIdValidation()
        {
            RuleFor(x=>x.CategoryId).NotEmpty().WithMessage("Category Id cannot be empty");
        }
    }
}