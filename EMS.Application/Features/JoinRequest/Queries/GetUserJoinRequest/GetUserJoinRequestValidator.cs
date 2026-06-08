using FluentValidation;

namespace EMS.Application.Features.JoinRequest.Queries.GetUserJoinRequest
{
    public class GetUserJoinRequestValidator:AbstractValidator<GetUserJoinRequestQuery>
    {
        public GetUserJoinRequestValidator()
        {
            RuleFor(x=>x.UserId).Must(x=>!string.IsNullOrWhiteSpace(x)).WithMessage("User Id cannot be empty");
        }
    }
}
