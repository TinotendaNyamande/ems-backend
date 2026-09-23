using FluentValidation;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditById
{
    public class GetAuditTrailForTaskValidation:AbstractValidator<GetAuditByIdQuery>
    {
        public GetAuditTrailForTaskValidation()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}