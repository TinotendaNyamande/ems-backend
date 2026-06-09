using EMS.API.Filters;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret;
using EMS.Application.Features.EmailConfigs.Commands.ChangeEmailPassword;
using EMS.Application.Features.EmailConfigs.Commands.CreateEmailConfig;
using EMS.Application.Features.EmailConfigs.Commands.DeleteEmail;
using EMS.Application.Features.EmailConfigs.Commands.TestEmail;
using EMS.Application.Features.EmailConfigs.Commands.ValidateConfig;
using EMS.Application.Features.EmailConfigs.Queries.GetEmail;
using EMS.Application.Features.EmailConfigs.Queries.GetEmailForOrganisation;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailConfigurationController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        [RequirePermission(PermissionKeys.MailBoxesView)]
        public async Task<ActionResult<EmailConfigDto>> GetEmailConfig(Guid id)
        {
            var emailDto= await mediator.Send (new GetEmailQuery(id));
            return Ok(emailDto);
        }
        [HttpGet("organisation/{organisationId}")]
        [RequirePermission(PermissionKeys.MailBoxesView)]
        public async Task<ActionResult<IEnumerable<EmailConfigDto>>> GetEmailConfigs(Guid organisationId)
        {
            var emailsDto=await mediator.Send(new GetEmailForOrganisationQuery(organisationId));
            return Ok(emailsDto);
        }
        [HttpPost]
        [RequirePermission(PermissionKeys.MailBoxesCreate)]
        public async Task<IActionResult> CreateEmailConfig([FromBody]  CreateEmailConfigCommand command)
        {
            var emailDto = await mediator.Send(command);
            return CreatedAtAction("GetEmailConfig",new {id=emailDto.Id},emailDto);
        }
        [HttpDelete("{id}")]
        [RequirePermission(PermissionKeys.MailBoxesDelete)]
        public async Task<IActionResult> DeleteEmail(Guid id)
        {
            await mediator.Send(new DeleteEmailCommand(id));
            return NoContent();
        }
        [HttpPatch("change-password/{id}")]
        [RequirePermission(PermissionKeys.MailBoxesEdit)]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangeEmailPasswordCommand command)
        {
            var updatedCommand = command with { EmailId = id};
            await mediator.Send(updatedCommand);
            return NoContent(); 
        }
        [HttpPatch("change-client-secret/{id}")]
        [RequirePermission(PermissionKeys.MailBoxesEdit)]
        public async Task<IActionResult> ChangeClientSecret(Guid id, [FromBody] ChangeClientSecretCommand command)
        {
            var updatedCommand = command with { EmailId = id};
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpGet("{id}/validate-email")]
        [RequirePermission(PermissionKeys.MailBoxesCreate)]
        public async Task<IActionResult> ValidateEmail(Guid id)
        {
            var isValid = await mediator.Send(new ValidateConfigCommand(id));
            return Ok(isValid);
        }
        [HttpPost("{id}/test-email")]
        [RequirePermission(PermissionKeys.MailBoxesCreate)]
        public async Task<IActionResult> TestEmail(Guid id, [FromBody] TestEmailCommand command)
        {
            var updatedCommand = command with { Id = id };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
    }
}
