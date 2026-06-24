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
        Task ChangeTaskStatusAsync (Guid id,TaskStatusList newStatus);
        Task<GetTasksDto> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<GetTasksDto>> GetTasksByUserId(string userId,TaskStatusList? status=null);
        Task<IEnumerable<GetTasksDto>> GetTasksForOrganisation(Guid organisationId,TaskStatusList? status=null);
        Task EditAdditionalInformation (Guid id,string additionalInfo);

    }
}