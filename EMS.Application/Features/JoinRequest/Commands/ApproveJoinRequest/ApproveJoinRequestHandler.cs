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
        //ILogger<ApproveJoinRequestHandler> logger
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
            //logger.LogInformation("Approving join request {RequestId} for user {UserId} with role {RoleId}", request.RequestId, user.Id, role.Id);
            var organisationUserRole = new OrganisationUserRole(requestToApprove.RequestById, role.Id);
            //logger.LogInformation("Adding role {RoleId} to user {UserId} in organisation {OrganisationId}", role.Id, user.Id, request.OrganisationId);
            await organisationUserRoleRepository.AddRoleToUserAsync(organisationUserRole);
            await userService.AddUserToOrganisationAsync(request.OrganisationId, requestToApprove.RequestById);
           // logger.LogInformation("User {UserId} added to organisation {OrganisationId}", user.Id, request.OrganisationId);
            await joinRequests.ApproveJoinRequestAsync(request.RequestId, request.ApprovingUserId);
           // logger.LogInformation("Join request {RequestId} approved by user {UserId}", request.RequestId, request.ApprovingUserId);
        }
    }
}
