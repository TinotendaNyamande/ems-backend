using EMS.Application.Dtos.Tasks;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task CreateTask(EmailTask task);
        Task<GetTasksDto> GetTaskById(Guid id);
        Task DeleteTask (Guid id);
        Task<IEnumerable<GetTasksDto>> GetTasksByUser (string userId,string? status);
        Task<IEnumerable<GetTasksDto>> GetTasksByOrganisation (string userId,string? status);
    }
}