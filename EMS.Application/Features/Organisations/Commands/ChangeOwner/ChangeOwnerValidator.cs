using FluentValidation;

namespace EMS.Application.Features.Organisations.Commands.ChangeOwner
{
    public class ChangeOwnerValidator:AbstractValidator<ChangeOwnerCommand>
    {
        public ChangeOwnerValidator()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
            RuleFor(x=>x.NewOwnerId).NotEmpty().WithMessage("Newowner id cannot be empty");
        }
    }
}
