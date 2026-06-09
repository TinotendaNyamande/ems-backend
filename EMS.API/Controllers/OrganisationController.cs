using EMS.API.Filters;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.Organisations.Commands.ChangeOwner;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using EMS.Application.Features.Organisations.Commands.DeleteOrganisationById;
using EMS.Application.Features.Organisations.Commands.RenameOrganisation;
using EMS.Application.Features.Organisations.Queries.GetOrganisationById;
using EMS.Application.Features.Organisations.Queries.GetOrganisations;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController(IMediator mediat) : ControllerBase
    {
        [HttpGet]
        [RequirePermission(PermissionKeys.OrganisationView)]
        public async Task<ActionResult<IEnumerable<OrganisationDto>>> GetAll()
        {
            var organisations =  await mediat.Send(new GetOrganisationsQuery());
            return Ok(organisations);
        }
        [HttpGet("{id}")]
        [RequirePermission(PermissionKeys.OrganisationView)]
        public async Task<ActionResult<OrganisationDetailsDto>> GetById(Guid id)
        {
            var organisation = await mediat.Send(new GetOrganisationByIdQuery(id));
            return Ok(organisation);
        }
        [HttpDelete("{id}")]
        [RequirePermission(PermissionKeys.OrganisationDelete)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await mediat.Send(new DeleteOrganisationByIdCommand(id));
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrganisation([FromBody] CreateOrganisationCommand command)
        {
            var org = await mediat.Send(command);
            return CreatedAtAction("GetById", new { id = org.Id }, org);
        }
        [HttpPatch("change-owner/{id}")]
        [RequirePermission(PermissionKeys.OrganisationEdit)]
        public async Task<IActionResult> ChangeOwner(Guid id,[FromBody] ChangeOwnerCommand command)
        {
            var updatedCommand = command with { OrganisationId = id };
            await mediat.Send(updatedCommand);
            return NoContent();
        }
        [HttpPatch("rename/{id}")]
        [RequirePermission(PermissionKeys.OrganisationEdit)]
        public async Task<IActionResult> Rename(Guid id, [FromBody] RenameOrganisationCommand command)
        {
            var updatedCommand = command with { OrganisationId = id }; 
            await mediat.Send(updatedCommand);
            return NoContent();
        }
    }
}
