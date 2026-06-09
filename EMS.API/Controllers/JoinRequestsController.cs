using EMS.API.Filters;
using EMS.Application.Features.JoinRequest.Commands.ApproveJoinRequest;
using EMS.Application.Features.JoinRequest.Commands.CreateJoinRequest;
using EMS.Application.Features.JoinRequest.Commands.DeleteJoinRequest;
using EMS.Application.Features.JoinRequest.Commands.RejectJoinRequest;
using EMS.Application.Features.JoinRequest.Queries.GetAllJoinRequests;
using EMS.Application.Features.JoinRequest.Queries.GetPendingJoinRequests;
using EMS.Application.Features.JoinRequest.Queries.GetUserJoinRequest;
using EMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinRequestsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateJoinRequest([FromBody] CreateJoinRequestCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
        [HttpGet("pending/{organisationId}")]
        [RequirePermission(PermissionKeys.JoinRequestsView)]
        public async Task<IActionResult> GetPendingJoinRequests(Guid organisationId)
        {
            var query = new GetPendingJoinRequestsQuery(organisationId );
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("all/{organisationId}")]
        [RequirePermission(PermissionKeys.JoinRequestsView)]
        public async Task<IActionResult> GetAllJoinRequests(Guid organisationId)
        {
            var query = new GetAllJoinRequestsQuery(organisationId);
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [HttpDelete("{joinRequestId}")]
        public async Task<IActionResult> DeleteJoinRequest(Guid joinRequestId)
        {
            var command = new DeleteJoinRequestCommand(joinRequestId);
            await mediator.Send(command);
            return Ok();
        }
        [HttpPost("approve/{joinRequestId}")]
        [RequirePermission(PermissionKeys.JoinRequestsApprove)]
        public async Task<IActionResult> ApproveJoinRequest(Guid joinRequestId, [FromBody] ApproveJoinRequestCommand command)
        {
            var updatedCommand = command with { RequestId = joinRequestId };
            await mediator.Send(updatedCommand);
            return Ok();
        }
        [HttpPost("reject/{joinRequestId}")]
        [RequirePermission(PermissionKeys.JoinRequestsApprove)]
        public async Task<IActionResult> RejectJoinRequest(Guid joinRequestId, [FromBody] RejectJoinRequestCommand command)
        {
            var updatedCommand = command with { RequestId = joinRequestId };
            await mediator.Send(updatedCommand);
            return Ok();
        }
        [HttpGet("user/{userId}")]
        [RequirePermission(PermissionKeys.JoinRequestsView)]
        public async Task<IActionResult> GetUserJoinRequests(string userId)
        {
            var query = new GetUserJoinRequestQuery(userId);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
