using FluentValidation;

namespace EMS.Application.Features.SLAEntriesTracking.Queries.GetRunningSLAEntryForTask
{
    public class GetRunningSLAEntryForTaskValidation:AbstractValidator<GetRunningSLAEntryForTaskQuery>
    {
        public GetRunningSLAEntryForTaskValidation()
        {
            RuleFor(x=>x.TaskId).NotEmpty().WithMessage("Task id cannot be empty");
        }
    }
}