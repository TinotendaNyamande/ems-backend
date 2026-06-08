using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Commands.ApproveJoinRequest
{
    public class ApproveJoinRequestValidator:AbstractValidator<ApproveJoinRequestCommand>
    {
        public ApproveJoinRequestValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty()
                .WithMessage("Join request ID is required.");
            RuleFor(x=>x.ApprovingUserId)
                .Must(x=>!string.IsNullOrWhiteSpace(x))
                .WithMessage("Approving user ID is required.");
            RuleFor(x => x.OrganisationId)
                .NotEmpty()
                .WithMessage("Organisation ID is required.");
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Role ID is required.");
        }
    }
}
