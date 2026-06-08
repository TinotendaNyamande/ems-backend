using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Commands.DeleteJoinRequest
{
    public class DeleteJoinRequestValidator:AbstractValidator<DeleteJoinRequestCommand>
    {
        public DeleteJoinRequestValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty()
                .WithMessage("Join request ID is required.");
        }
    }
}
