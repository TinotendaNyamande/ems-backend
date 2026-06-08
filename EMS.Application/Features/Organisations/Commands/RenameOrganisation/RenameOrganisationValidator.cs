using FluentValidation;

namespace EMS.Application.Features.Organisations.Commands.RenameOrganisation
{
    public class RenameOrganisationValidator:AbstractValidator<RenameOrganisationCommand>
    {
        public RenameOrganisationValidator()
        {
            RuleFor(x => x.NewOrganisationName).NotEmpty().MinimumLength(3).WithMessage("Organisation name must be at least 3 characters long");
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("OrganisationId must not be empty");
        }
    }
}
