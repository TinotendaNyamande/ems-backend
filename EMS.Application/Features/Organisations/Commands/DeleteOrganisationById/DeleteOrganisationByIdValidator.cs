using FluentValidation;

namespace EMS.Application.Features.Organisations.Commands.DeleteOrganisationById
{
    public class DeleteOrganisationByIdValidator:AbstractValidator<DeleteOrganisationByIdCommand>
    {
        public DeleteOrganisationByIdValidator()
        {
            RuleFor(x=>x.OrganisationId).NotEmpty().WithMessage("Organisation id cannot be empty");
        }
    }
}
