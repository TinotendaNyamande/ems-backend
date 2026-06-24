using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix;
using EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix;
using EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById;
using EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForOrganisation;
using EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixForUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCategoryUserMatrixController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMatrixCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteMatrixCommand(id));
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMatrixDto>> GetById(Guid id)
        {
            var matrix = await mediator.Send(new GetMatrixByIdQuery(id));
            return Ok(matrix);
        }
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<GetMatrixDto>>> GetByUser(string userId)
        {
            var matrix = await mediator.Send(new GetMatrixForUserQuery(userId));
            return Ok(matrix);
        }
        [HttpGet("by-organisation/{organisationId}")]
        public async Task<ActionResult<IEnumerable<GetMatrixDto>>> GetByOrganisation(Guid organisationId)
        {
            var matrix = await mediator.Send(new GetMatrixForOrganisationQuery(organisationId));
            return Ok(matrix);
        }

    }
}