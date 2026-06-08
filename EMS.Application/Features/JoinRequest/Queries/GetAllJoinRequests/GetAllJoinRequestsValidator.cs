using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Queries.GetAllJoinRequests
{
    public class GetAllJoinRequestsValidator:AbstractValidator<GetAllJoinRequestsQuery>
    {
        public GetAllJoinRequestsValidator()
        {
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("Organisation Id is required.");
        }
    }
}
