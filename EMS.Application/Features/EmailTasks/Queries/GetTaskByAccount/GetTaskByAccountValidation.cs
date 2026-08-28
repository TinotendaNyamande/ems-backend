using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAccount
{
    public class GetTaskByAccountValidation : AbstractValidator<GetTaskByAccountQuery>
    {
        public GetTaskByAccountValidation()
        {
            RuleFor(x => x.EmailAccountId).NotEmpty().WithMessage("Email account ID cannot be empty");
        }
    }
}
