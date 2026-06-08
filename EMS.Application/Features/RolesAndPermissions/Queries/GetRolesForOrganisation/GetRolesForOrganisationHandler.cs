using AutoMapper;
using EMS.Application.Dtos.RolesAndPermissions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.RolesAndPermissions.Queries.GetRolesForOrganisation
{
    internal class GetRolesForOrganisationHandler(IRolesRepository rolesRepository, IMapper mapper) : IRequestHandler<GetRolesForOrganisationQuery, IEnumerable<GetRoleDto>>
    {
        public async Task<IEnumerable<GetRoleDto>> Handle(GetRolesForOrganisationQuery request, CancellationToken cancellationToken)
        {
            var roles = await rolesRepository.GetRolesForOrganisationAsync(request.OrganisationId);
            return mapper.Map<IEnumerable<GetRoleDto>>(roles);

        }
    }
}
