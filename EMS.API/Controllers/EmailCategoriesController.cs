using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory;
using EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory;
using EMS.Application.Features.EmailCategories.Commands.EditEmailCategory;
using EMS.Application.Features.EmailCategories.Queries.GetCategoryByName;
using EMS.Application.Features.EmailCategories.Queries.GetEmailCategoriesForAccount;
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
            var emailCategory = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = emailCategory.Id }, emailCategory);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditEmailCategory(Guid id, EditEmailCategoryCommand command)
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
        public async Task<ActionResult<GetEmailCategoryDto>> GetById(Guid id)
        {
            var emailCategory = await mediator.Send(new GetEmailCategoryByIdQuery(id));
            return Ok(emailCategory);
        }
                [HttpGet("by-name/{categoryName}")]
        public async Task<ActionResult<GetEmailCategoryDto>> GetByName(string categoryName)
        {
            var emailCategory = await mediator.Send(new GetCategoryByNameQuery(categoryName));
            return Ok(emailCategory); 
        }
        [HttpGet("by-email-account/{emailAccountId}")]
        public async Task<ActionResult<IEnumerable<GetEmailCategoryDto>>> GetAll(Guid emailAccountId)
        {
            var categories = await mediator.Send(new GetEmailCategoriesForAccountQuery(emailAccountId));
            return Ok(categories);
        }
    }
}