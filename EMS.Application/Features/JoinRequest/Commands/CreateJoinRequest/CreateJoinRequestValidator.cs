using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Commands.CreateJoinRequest
{
    public class CreateJoinRequestValidator:AbstractValidator<CreateJoinRequestCommand>
    {
        public CreateJoinRequestValidator()
        {
            RuleFor(x => x.RequestById)
                .Must(x=> !string.IsNullOrWhiteSpace(x))
                .WithMessage("RequestById is required.");
            RuleFor(x => x.OrganisationId)
                .NotEmpty()
                .WithMessage("OrganisationId is required.");
        }
    }
}
