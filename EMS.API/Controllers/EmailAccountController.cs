using EMS.API.Filters;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Features.EmailAccounts.Commands.ChangeClientSecret;
using EMS.Application.Features.EmailAccounts.Commands.ChangeEmailAccountPassword;
using EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount;
using EMS.Application.Features.EmailAccounts.Commands.DeleteEmailAccount;
using EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount;
using EMS.Application.Features.EmailAccounts.Commands.ValidateEmailAccount;
using EMS.Application.Features.EmailAccounts.Queries.GetEmailAccount;
using EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountsForOrganisation;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailAccountController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        [RequirePermission(PermissionKeys.MailBoxesView)]
        public async Task<ActionResult<EmailAccountDto>> GetEmailAccount(Guid id)
        {
            var emailDto= await mediator.Send (new GetEmailAccountQuery(id));
            return Ok(emailDto);
        }
        [HttpGet("organisation/{organisationId}")]
        [RequirePermission(PermissionKeys.MailBoxesView)]
        public async Task<ActionResult<IEnumerable<EmailAccountDto>>> GetEmailAccounts(Guid organisationId)
        {
            var emailsDto=await mediator.Send(new GetEmailAccountsForOrganisationQuery(organisationId));
            return Ok(emailsDto);
        }
        [HttpPost]
        [RequirePermission(PermissionKeys.MailBoxesCreate)]
        public async Task<IActionResult> CreateEmailAccount([FromBody]  CreateEmailAccountCommand command)
        {
            var emailDto = await mediator.Send(command);
            return CreatedAtAction("GetEmailAccount",new {id=emailDto.Id},emailDto);
        }
        [HttpDelete("{id}")]
        [RequirePermission(PermissionKeys.MailBoxesDelete)]
        public async Task<IActionResult> DeleteEmail(Guid id)
        {
            await mediator.Send(new DeleteEmailAccountCommand(id));
            return NoContent();
        }
        [HttpPatch("change-password/{id}")]
        [RequirePermission(PermissionKeys.MailBoxesEdit)]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangeEmailAccountPasswordCommand command)
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
            var isValid = await mediator.Send(new ValidateAccountCommand(id));
            return Ok(isValid);
        }
        [HttpPost("{id}/test-email")]
        [RequirePermission(PermissionKeys.MailBoxesCreate)]
        public async Task<IActionResult> TestEmail(Guid id, [FromBody] TestEmailAccountCommand command)
        {
            var updatedCommand = command with { Id = id };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
    }
}
