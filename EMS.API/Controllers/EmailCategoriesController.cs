using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory;
using EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory;
using EMS.Application.Features.EmailCategories.Commands.RenameEmailCategory;
using EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForOrganisation;
using EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEmailCategory([FromBody] CreateEmailCategoryCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> RenameCategory(Guid id, RenameEmailCategoryCommand command)
        {
            var updatedCommand = command with
            {
                Id = id
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailCategory(Guid id,[FromQuery] Guid? newCategoryId)
        {
            await mediator.Send(new DeleteEmailCategoryCommand(id,newCategoryId));
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmailCategoriesDto>> GetById(Guid id)
        {
            var emailCategory = await mediator.Send(new GetEmailCategoryByIdQuery(id));
            return Ok(emailCategory);
        }
        [HttpGet("by-organisation/{organisationId}")]
        public async Task<ActionResult<IEnumerable<GetEmailCategoriesDto>>> GetByOrganisation(Guid organisationId)
        {
            var categories = await mediator.Send(new GetEmailCategoriesForOrganisationQuery(organisationId));
            return Ok(categories);
        }
    }
}