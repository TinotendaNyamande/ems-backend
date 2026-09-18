using EMS.Application.Dtos.Tasks;
using EMS.Domain.Enums;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailTasksRepository
    {
        Task CreateTaskAsync (EmailTask emailTask);
        Task DeleteTaskAsync (Guid id);
        Task ReassignTaskAsync(Guid id,string newUserId);
        Task CloseTaskAsync(Guid id,string additionalInformation);
        Task ChangeTaskStatusAsync (Guid id,TaskStatusList newStatus,string? additionalInformation=null);
        Task<GetTasksDto> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<GetTasksDto>> GetTasksByUserIdAsync(string userId,TaskStatusList? status=null);
        Task<IEnumerable<GetTasksDto>> GetTasksForAccountAsync(Guid emailAccountId,TaskStatusList? status=null);
        Task EditAdditionalInformationAsync (Guid id,string additionalInfo);
        Task ReOpenTaskAsync(Guid id);
         Task<UserTasksSummaryDto> GetUserTasksSummaryAsync(string userId);
         Task<IEnumerable<GetTasksDto>> GetAllTasksAsync();
         Task<IEnumerable<GetTasksDto>> GetAllOpenTasksAsync();
         Task<IEnumerable<GetTasksDto>> GetAllOpenTasksByUserIdAsync(string userId);
        

    }
}