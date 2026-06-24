using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByAssignedUser
{
    public class GetTaskByAssignedUserValidation : AbstractValidator<GetTaskByAssignedUserQuery>
    {
        public GetTaskByAssignedUserValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID cannot be empty");
        }
    }
}
