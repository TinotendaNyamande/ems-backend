using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Queries.GetPendingJoinRequests
{
    public class GetPendingJoinRequestsValidator:AbstractValidator<GetPendingJoinRequestsQuery>
    {
        public GetPendingJoinRequestsValidator()
        {
            RuleFor(x => x.OrganisationId).NotEmpty().WithMessage("Organisation Id is required.");
        }
    }
}
