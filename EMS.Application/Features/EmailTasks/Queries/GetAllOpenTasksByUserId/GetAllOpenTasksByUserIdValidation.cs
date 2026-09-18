using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetAllOpenTasksByUserId
{
    public class GetAllOpenTasksByUserIdValidation :AbstractValidator<GetAllOpenTasksByUserIdQuery>
    {
        public GetAllOpenTasksByUserIdValidation()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("User ID cannot be empty");
        }
    }
}
