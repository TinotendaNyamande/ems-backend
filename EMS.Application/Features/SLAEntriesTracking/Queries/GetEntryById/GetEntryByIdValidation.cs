using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetEntryById
{
    public class GetEntryByIdValidation : AbstractValidator<GetEntryByIdQuery>
    {
        public GetEntryByIdValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}