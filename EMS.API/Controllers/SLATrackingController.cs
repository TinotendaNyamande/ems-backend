namespace EMS.API.Controllers
{
    using EMS.Application.Features.SLAEntriesTracking.Commands.DeleteEntry;
    using EMS.Application.Features.SLAEntriesTracking.Commands.UpdateEntry;
    using EMS.Application.Features.SLAEntriesTracking.Queries.GetEntriesForTask;
    using EMS.Application.Features.SLAEntriesTracking.Queries.GetEntryById;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SLATrackingController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetEntryByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("emailTask/{emailTaskId}")]
        public async Task<IActionResult> GetByEmailTaskIdAsync(Guid emailTaskId)
        {
            var query = new GetEntriesForTaskQuery(emailTaskId);
            var result = await mediator.Send(query);
            return Ok(result);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEntryCommand slaTracking)
        {
            var updatedCommand = slaTracking with { Id = id };
            await mediator.Send(updatedCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteEntryCommand(id);
            await mediator.Send(command);
            return NoContent();
        }

    }
}