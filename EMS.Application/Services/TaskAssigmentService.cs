using EMS.Application.Interfaces;
using EMS.Domain.Models;

namespace EMS.Application.Services
{
    internal class TaskAssignmentService : ITaskAssignmentService
    {
        public async Task<EmailCategoriesUserMatrix> PickUserAsync(IEnumerable<EmailCategoriesUserMatrix> matrix)
        {
            var candidates = matrix.Where(x => x.IsAvailable).ToList();

            if (candidates.Count == 0)
                throw new InvalidOperationException("No available users found for this category.");

            var selected = candidates
                .OrderBy(x => x.LastAssignedAt == default ? DateTime.MinValue : x.LastAssignedAt)
                .ThenBy(x => x.Id)
                .First();

            return selected;
        }
    }
}