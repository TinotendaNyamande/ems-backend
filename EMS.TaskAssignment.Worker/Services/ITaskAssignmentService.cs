namespace EMS.TaskAssignment.Worker.Services
{
    public interface ITaskAssignmentService
    {
        Task ProcessAsync(CancellationToken cancellationToken);
    }
}