using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Expenses.API.Application.Commands;
using MediatR;
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
        
        //todo should be getId
        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(null);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateExpenseCommand createExpenseCommand)
        {
            var id = await _mediator.Send(createExpenseCommand);
            return Ok(id);
        }
    }
}