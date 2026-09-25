using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface ITaskAssignmentService
    {
        Task<EmailCategoriesUserMatrix> PickUserAsync(IEnumerable<EmailCategoriesUserMatrix> matrix);
    }
}