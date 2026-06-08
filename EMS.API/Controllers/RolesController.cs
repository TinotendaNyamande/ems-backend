using EMS.Application.Features.RolesAndPermissions.Commands.EditPermission;
using EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionById;
using EMS.Application.Features.RolesAndPermissions.Queries.GetPermissionsForRole;
using EMS.Application.Features.RolesAndPermissions.Queries.GetRolesForOrganisation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("permissions{id}")]
        public async Task<IActionResult> GetPermissions(Guid id)
        {
            var result = await mediator.Send(new GetPermissionByIdQuery(id));
            return Ok(result);

        }
        [HttpGet("permissions/{roleId}")]
        public async Task<IActionResult> GetPermissionsForRole(Guid roleId)
        {
            var result = await mediator.Send(new GetPermissionsForRoleQuery(roleId));
            return Ok(result);
        }
        [HttpGet("organisation/{organisationId}")]
        public async Task<IActionResult> GetRolesForOrganisation(Guid organisationId)
        {
            var result = await mediator.Send(new GetRolesForOrganisationQuery(organisationId));
            return Ok(result);
        }
        [HttpPatch("permissions/update/{permissionId}")]
        public async Task<IActionResult> UpdatePermission(Guid permissionId, [FromBody] EditPermissionCommand command)
        {
            var updatedCommand = command with { PermissionId = permissionId };
             await mediator.Send(updatedCommand);
            return NoContent();
        }
    }
}
