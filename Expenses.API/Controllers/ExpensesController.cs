using System.Runtime.InteropServices.ComTypes;
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
    public class ExpensesController : Controller
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var expense = await _mediator.Send(new GetExpenseByIdQuery(id));
            if (expense == null) return NotFound();
            return Ok(expense);
        }
        

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchExpense(string id, UpdateExpenseCommand expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(expense);
            return NoContent();
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            var command = new DeleteExpenseCommand(id);
            var foundAndDeleted = await _mediator.Send(command);

            if (!foundAndDeleted) return NotFound();
            return Ok();
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateExpenseCommand expense)
        {
            var id = await _mediator.Send(expense);
            return Ok( new {id} );
        }
    }
}