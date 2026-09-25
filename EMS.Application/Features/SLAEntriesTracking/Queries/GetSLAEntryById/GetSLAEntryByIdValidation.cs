using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetSLAEntryById
{
    public class GetSLAEntryByIdValidation : AbstractValidator<GetSLAEntryByIdQuery>
    {
        public GetSLAEntryByIdValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}