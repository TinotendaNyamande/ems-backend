using MediatR;
using Microsoft.AspNetCore.Mvc;
using EMS.Application.Features.UsersManagement.Commands.CreateUser;
using EMS.Application.Features.UsersManagement.Queries.GetUserById;
using EMS.Application.Features.UsersManagement.Queries.GetAllUsers;
using EMS.Application.Features.UsersManagement.Commands.ChangeRole;


namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator,ILogger<UsersController> logger) : ControllerBase
    {
        [HttpGet("{userId}")]

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
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
        {
            logger.LogInformation("Create user endpoint called for email: {Email}", request.Email);
            await mediator.Send(request);
            logger.LogInformation("Create user endpoint completed for email: {Email})", request.Email);
            return CreatedAtAction(nameof(GetUserInfo), new { userId = request.Email }, null);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users =  await mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        
        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleCommand request)
        {
            logger.LogInformation("Change role endpoint called for userId: {UserId} and role: {Role}", request.UserId, request.NewRole);
            await mediator.Send(request);
            logger.LogInformation("Change role endpoint completed for userId: {UserId} and role: {Role}", request.UserId, request.NewRole);
            return Ok();
        }
    }
}
