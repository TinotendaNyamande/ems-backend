using MediatR;
using Microsoft.AspNetCore.Mvc;
using EMS.Application.Features.UsersManagement.Commands.CreateUserForOrganisation;
using EMS.Application.Features.UsersManagement.Queries.GetUserByIdQuery;
using EMS.Application.Features.UsersManagement.Queries.GetUsersForOrganisationQuery;
using EMS.Application.Features.UsersManagement.Commands.AssignUserRole;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator,ILogger<UsersController> logger) : ControllerBase
    {
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUserInfo(string userId)
        {
            logger.LogInformation("Get user info endpoint called for userId: {UserId}", userId);
            var userInfo = await mediator.Send(new GetUserByIdQuery(userId));
            if (userInfo == null)
            {
                logger.LogWarning("Get user info endpoint did not find user with id: {UserId}", userId);
                return NotFound(new { message = "User not found." });
            }
            logger.LogInformation("Get user info endpoint completed for userId: {UserId}", userId);
            return Ok(userInfo);
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserForOrganisationCommand request)
        {
            logger.LogInformation("Create user endpoint called for email: {Email}", request.Email);
            await mediator.Send(request);
            logger.LogInformation("Create user endpoint completed for email: {Email})", request.Email);
            return Ok();
        }
        [HttpGet("organisation/{organisationId}")]
        public async Task<IActionResult> GetUsersByOrganisation(string organisationId)
        {
            logger.LogInformation("Get users by organisation endpoint called for organisationId: {OrganisationId}", organisationId);
            var users = await mediator.Send(new GetUsersForOrganisationQuery(Guid.Parse(organisationId)));
            logger.LogInformation("Get users by organisation endpoint completed for organisationId: {OrganisationId}", organisationId);
            return Ok(users);
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignUserRoleCommand request)
        {
            logger.LogInformation("Assign role endpoint called for userId: {UserId} and role: {Role}", request.UserId, request.RoleId);
            await mediator.Send(request);
            logger.LogInformation("Assign role endpoint completed for userId: {UserId} and role: {Role}", request.UserId, request.RoleId);
            return Ok();
        }
    }
}
