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
    public class IncomeController : Controller
    {
        private readonly IMediator _mediator;

        public IncomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetIncomeByIdQuery(id);
            var income = await _mediator.Send(query);

            if (income == null) return NotFound();
            return Ok(income);
        }
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PathIncome(string id, UpdateIncomeCommand income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(income);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(string id)
        {
            var command = new DeleteIncomeCommand(id);
            var foundAndDeleted = await _mediator.Send(command);

            if (!foundAndDeleted) return NotFound();
            return Ok();
        }
        
        
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateIncomeCommand income)
        {
            if (ModelState.IsValid)
            {
                var incomeId = await _mediator.Send(income);
                if (incomeId == null) return BadRequest("An error occured");

                return Ok(new {id = incomeId });
            }
            else return BadRequest("Error");
        }
    }
}