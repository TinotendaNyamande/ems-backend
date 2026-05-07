using FluentValidation;

namespace EMS.Application.Features.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationValidator : AbstractValidator<CreateOrganisationCommand>
    {
        public CreateOrganisationValidator()
        {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Organisation name must not be empty")
                .MinimumLength(3)
                .WithMessage("Organisation name must be at least 3 characters long");
            RuleFor(x => x.OwnerId)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Owner Id must not be empty");
        }
    }
}
