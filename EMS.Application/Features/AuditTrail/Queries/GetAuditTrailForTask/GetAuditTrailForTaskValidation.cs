using FluentValidation;

namespace EMS.Application.Features.AuditTrail.Queries.GetAuditTrailForTask
{
    public class GetAuditTrailForTaskValidation:AbstractValidator<GetAuditTrailForTaskQuery>
    {
        public GetAuditTrailForTaskValidation()
        {
            RuleFor(x=>x.TaskId).NotEmpty().WithMessage("Task Id cannot be empty");
        }
    }
}