using FluentValidation;

namespace EMS.Application.Features.UsersManagement.Queries.GetUserByIdQuery
{
    public class GetUserByIdValidator:AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
