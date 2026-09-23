using FluentValidation;

namespace EMS.Application.Features.Emails.Queries.GetEmailsPendingAssignment
{
    public class GetEmailsPendingAssignmentValidation:AbstractValidator<GetEmailsPendingAssignmentQuery>
    {
        public GetEmailsPendingAssignmentValidation()
        {
            RuleFor(x=>x.EmailAccountId).NotEmpty().WithMessage("Email account id cannot be empty");
        }
    }
}