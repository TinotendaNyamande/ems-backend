namespace EMS.TaskAssignment.Worker.Services
{
    public interface ITaskAssignmentService
    {
        Task<int> ProcessAsync(CancellationToken cancellationToken);
    }
}