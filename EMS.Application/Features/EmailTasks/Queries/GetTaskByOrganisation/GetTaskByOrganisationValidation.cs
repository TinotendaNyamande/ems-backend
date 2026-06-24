using FluentValidation;

namespace EMS.Application.Features.EmailTasks.Queries.GetTaskByOrganisation
{
    public class GetTaskByOrganisationValidation : AbstractValidator<GetTaskByOrganisationQuery>
    {
        public GetTaskByOrganisationValidation()
        {
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("Organisation ID cannot be empty");
        }
    }
}
