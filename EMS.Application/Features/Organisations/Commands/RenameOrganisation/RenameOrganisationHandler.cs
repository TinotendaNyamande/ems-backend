using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Commands.RenameOrganisation
{
    public class RenameOrganisationHandler(IOrganisationRepository organisationRepository) : IRequestHandler<RenameOrganisationCommand>
    {
        public async Task Handle(RenameOrganisationCommand request, CancellationToken cancellationToken)
        {
            await organisationRepository.RenameOrganisationAsync(request.OrganisationId, request.OrganisationNewName);
        }
    }
}
