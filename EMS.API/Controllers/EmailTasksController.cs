using EMS.Application.Dtos.Tasks;
using EMS.Application.Features.EmailTasks.Commands.AddNotes;
using EMS.Application.Features.EmailTasks.Commands.CloseTask;
using EMS.Application.Features.EmailTasks.Commands.DeleteTask;
using EMS.Application.Features.EmailTasks.Commands.ReassignTask;
using EMS.Application.Features.EmailTasks.Commands.ReOpenTask;
using EMS.Application.Features.EmailTasks.Commands.UpdateStatus;
using EMS.Application.Features.EmailTasks.Queries.GetTaskByAccount;
using EMS.Application.Features.EmailTasks.Queries.GetTaskByAssignedUser;
using EMS.Application.Features.EmailTasks.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTasksController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTasksDto>> GetById(Guid id)
        {
            var task = await mediator.Send(new GetTaskByIdQuery(id));
            return Ok(task);
        }
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<GetTasksDto>>> GetAssignedToUser(string userId)
        {
            var tasks = await mediator.Send(new GetTaskByAssignedUserQuery(userId));
            return Ok(tasks);
        }
        [HttpGet("by-email-account/{emailAccountId}")]
        public async Task<ActionResult<IEnumerable<GetTasksDto>>> GetAll(Guid emailAccountId)
        {
            var tasks = await mediator.Send(new GetTaskByAccountQuery(emailAccountId));
            return Ok(tasks);
        }
        [HttpPost("add-notes/{id}")]
        public async Task<IActionResult> AddNotes(Guid id, AddNotesCommand command)
        {
            var updatedCommand = command with
            {
                Id = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpPost("close-task/{id}")]
        public async Task<IActionResult> AddNotes(Guid id, CloseTaskCommand command)
        {
            var updatedCommand = command with
            {
                Id = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpPost("reassign-task/{id}")]
        public async Task<IActionResult> Reassign(Guid id, ReassignTaskCommand command)
        {
            var updatedCommand = command with
            {
                Id = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpPatch("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateStatusCommand command)
        {
            var updatedCommand = command with
            {
                Id = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {

            await mediator.Send(new DeleteTaskCommand(id));
            return NoContent();
        }
        [HttpPost("reopen-task/{id}")]
        public async Task<IActionResult> ReopenTask(Guid id, ReOpenTaskCommand command)
        {
            var updatedCommand = command with
            {
                TaskId = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }

    }
}
