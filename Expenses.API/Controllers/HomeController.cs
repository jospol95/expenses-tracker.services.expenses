using System.Threading.Tasks;
using Expenses.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getBudgetSummary")]
        public async Task<IActionResult> GetBudgetSummary(int month, int year, string userId)
        {
            var budgetSummary = await _mediator.Send(new GetBudgetSummaryQuery(month, year, userId));

            return Ok(budgetSummary);
        }
    }
}