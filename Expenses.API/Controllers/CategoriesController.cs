using System.Threading.Tasks;
using Expenses.API.Application.Commands;
using Expenses.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCategoriesForUser(int userId)
        {
            var query = new GetCategoriesQuery(userId);
            var categoriesList = await _mediator.Send(query);
            if (categoriesList == null) return NotFound();
            else return Ok(categoriesList);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCategoryCommand category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryId = await _mediator.Send(category);
            return Ok(categoryId);
        }
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(category);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        // Check the UserId
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleteCommand = new DeleteCategoryCommand(id);
            var foundAndDeleted = await _mediator.Send(deleteCommand);

            if (!foundAndDeleted) return NotFound();
            return Ok();

        }
    }
}