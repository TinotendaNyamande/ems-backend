using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Features.AuditTrail.Queries.GetAuditById;
using EMS.Application.Features.AuditTrail.Queries.GetAuditTrailForTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAuditController(IMediator mediator) : ControllerBase
    {
        [HttpGet("by-task/{taskId}")]
        public async Task<ActionResult<IEnumerable<GetTaskAuditTrailDto>>> GetAuditTrailForTask(Guid taskId)
        {
            var audits = await mediator.Send(new GetAuditTrailForTaskQuery(taskId));
            return Ok(audits);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuditTrailEntry([FromBody] CreateAuditTrailEntryCommand command)
        {
            var entry = await mediator.Send(command);
            return Ok(entry);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskAuditTrailDto>> GetAuditById(Guid id)
        {
            return await mediator.Send(new GetAuditByIdQuery(id));
        }
    }
}