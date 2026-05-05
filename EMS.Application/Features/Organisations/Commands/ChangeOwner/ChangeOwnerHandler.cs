using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Commands.ChangeOwner
{
    public class ChangeOwnerHandler(IOrganisationRepository organisationRepository) : IRequestHandler<ChangeOwnerCommand>
    {
        public async Task Handle(ChangeOwnerCommand request, CancellationToken cancellationToken)
        {
            await organisationRepository.ChangeOwnerAsync(request.OrganisationId, request.NewOwnerId);
        }
    }
}
