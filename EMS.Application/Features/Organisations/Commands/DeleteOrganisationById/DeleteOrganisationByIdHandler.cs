using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Organisations.Commands.DeleteOrganisationById
{
    public class DeleteOrganisationByIdHandler(IOrganisationRepository organisationRepository) : IRequestHandler<DeleteOrganisationByIdCommand>
    {
        public async Task Handle(DeleteOrganisationByIdCommand request, CancellationToken cancellationToken)
        {
            await organisationRepository.DeleteAsync(request.OrganisationId);
        }
    }
}
