using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskById
{
    public class GetTaskByIdValidation : AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty");
        }
    }
}
