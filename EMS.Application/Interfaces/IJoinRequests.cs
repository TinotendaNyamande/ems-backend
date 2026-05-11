using EMS.Application.Dtos.Auth;

namespace EMS.Application.Interfaces
{
    internal interface IJoinRequests
    {
        Task CreateJoinRequest(Guid organisationId, string userId);
        Task ApproveJoinRequest(Guid organisationId, string userId);
        Task RejectJoinRequest(Guid organisationId, string userId);
        Task<IEnumerable<UserDto>> GetPendingJoinRequestsAsync(Guid organisationId);
        Task<IEnumerable<UserDto>> GetAllJoinRequestsAsync(Guid organisationId);
    }
}
