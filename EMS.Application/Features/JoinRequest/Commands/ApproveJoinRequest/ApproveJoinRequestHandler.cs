using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EMS.Application.Features.JoinRequest.Commands.ApproveJoinRequest
{
    public class ApproveJoinRequestHandler
        (IJoinRequestsRepository joinRequests,
        IUserService userService,
        IOrganisationUserRoleRepository organisationUserRoleRepository,
        IRolesRepository rolesRepository
        ) : IRequestHandler<ApproveJoinRequestCommand>
    {
        public async Task Handle(ApproveJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(request.ApprovingUserId);
            if(user.OrganisationId != request.OrganisationId)
            {
                throw new UnauthorizedAccessException("User does not have permission to approve this join request.");
            }
            var requestToApprove = await joinRequests.GetJoinRequestByIdAsync(request.RequestId);
            await organisationUserRoleRepository.RemoveRolesFromUserAsync(requestToApprove.RequestById);
            var role = await rolesRepository.GetRoleByIdAsync(request.RoleId);
            var organisationUserRole = new OrganisationUserRole(requestToApprove.RequestById, role.Id);
            await organisationUserRoleRepository.AddRoleToUserAsync(organisationUserRole);
            await userService.AddUserToOrganisationAsync(request.OrganisationId, requestToApprove.RequestById);
            await joinRequests.ApproveJoinRequestAsync(request.RequestId, request.ApprovingUserId);
        }
    }
}
