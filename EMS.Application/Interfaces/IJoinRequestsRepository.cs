using EMS.Application.Dtos.Auth;
using EMS.Application.Dtos.JoinRequests;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IJoinRequestsRepository
    {
        Task CreateJoinRequestAsync(JoinRequest joinRequest);
        Task ApproveJoinRequestAsync(Guid requestId,string approvingUserId);
        Task RejectJoinRequestAsync(Guid requestId,string rejectingUserId);
        Task<IEnumerable<JoinRequestDto>> GetPendingJoinRequestsAsync(Guid organisationId);
        Task<IEnumerable<JoinRequestDto>> GetAllJoinRequestsAsync(Guid organisationId);
        Task DeleteRequestAsync(Guid requestId);
        Task<IEnumerable<JoinRequestDto>> GetUserJoinRequestsAsync(string userId);
        Task<IEnumerable<JoinRequestDto>> GetUserPendingJoinRequestsAsync(string userId);
        Task<JoinRequest?> GetJoinRequestByIdAsync(Guid requestId);
    }
}
