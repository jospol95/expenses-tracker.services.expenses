using System.Threading.Tasks;
using Expenses.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BudgetController : Controller
    {
        private readonly IMediator _mediator;

        public BudgetController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET
        [HttpGet("get")]
        public async Task<IActionResult> Get(int month, int year, string userId)
        {
            var request = new GetBudgetCalendarByDateQuery(month,year,userId);
            var budgetCalendarList = await _mediator.Send(request);
            
            if (budgetCalendarList == null) return BadRequest("Not found");
            return Ok(budgetCalendarList);
        }

        [HttpPost("getBudgetReport")]
        public async Task<IActionResult> GetBudgetReport(GetBudgetReportQuery request)
        {
            if (!ModelState.IsValid) return BadRequest("All are parameters required");
            
            var report = await _mediator.Send(request);
            if (report == null) return NotFound();

            return Ok(report);

        }

    }
}