using FluentValidation;

namespace EMS.Application.Features.EmailConfigs.Commands.DeleteEmail
{
    public class DeleteEmailValidator:AbstractValidator<DeleteEmailCommand>
    {
        public DeleteEmailValidator()
        {
            RuleFor(x=>x.EmailId).NotEmpty().WithMessage("Email id cannot be empty");
        }
    }
}
