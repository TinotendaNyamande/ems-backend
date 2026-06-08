using AutoMapper;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;
using MediatR;

namespace EMS.Application.Features.JoinRequest.Commands.CreateJoinRequest
{
    public class CreateJoinRequestHandler(IJoinRequestsRepository joinRequests,IUserService userService, IMapper mapper) : IRequestHandler<CreateJoinRequestCommand>
    {
        public async Task Handle(CreateJoinRequestCommand request, CancellationToken cancellationToken)
        {
            //check if user already belongs to organisation
            var user = await userService.GetUserByIdAsync(request.RequestById);
            if (user.OrganisationId == Guid.Empty)
            {
                throw new Exception("User already belongs to an organisation");
            }
            var pendingRequest = await joinRequests.GetUserPendingJoinRequestsAsync(request.RequestById);
            if(pendingRequest.Any())
            {
                throw new BusinessRuleException("User already has a pending join request");
            }

            var joinRequest  = mapper.Map<Domain.Models.JoinRequest>(request);
            await joinRequests.CreateJoinRequestAsync(joinRequest);
        }
    }
}
