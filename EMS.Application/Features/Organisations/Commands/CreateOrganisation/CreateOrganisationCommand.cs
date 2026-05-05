using EMS.Application.Dtos.Organisation;
using MediatR;

namespace EMS.Application.Features.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationCommand(string name,string ownerId):IRequest<OrganisationDto>
    {
        public string Name { get; init; } = name;
        public string OwnerId { get; init; } =ownerId;
    }
}
