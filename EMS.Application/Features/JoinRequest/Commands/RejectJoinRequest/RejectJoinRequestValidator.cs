using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Commands.RejectJoinRequest
{
    public class RejectJoinRequestValidator:AbstractValidator<RejectJoinRequestCommand>
    {
        public RejectJoinRequestValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty()
                .WithMessage("Join request ID is required.");
            RuleFor(x => x.RejectingUserId)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Rejecting user ID is required.");
            RuleFor(x => x.OrganisationId)
                .NotEmpty()
                .WithMessage("Organisation ID is required.");
        }
    }
}
