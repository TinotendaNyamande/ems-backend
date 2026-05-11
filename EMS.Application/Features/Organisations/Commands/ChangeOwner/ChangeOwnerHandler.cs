using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Commands.ChangeOwner
{
    public class ChangeOwnerHandler(IOrganisationRepository organisationRepository,IUserService userService) : IRequestHandler<ChangeOwnerCommand>
    {
        public async Task Handle(ChangeOwnerCommand request, CancellationToken cancellationToken)
        {
            var newOwner = await userService.GetUserByIdAsync(request.NewOwnerId);
            if(newOwner.OrganisationId != request.OrganisationId)
            {
                throw new Exception("New owner must be a member of the organisation");
            }
            await organisationRepository.ChangeOwnerAsync(request.OrganisationId, request.NewOwnerId);
        }
    }
}
