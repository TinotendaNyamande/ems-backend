using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetUserSummary
{
    public class GetUserSummaryValidation : AbstractValidator<GetUserSummaryQuery>
    {
        public GetUserSummaryValidation()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("User Id cannot be empty");
        }
    }
}