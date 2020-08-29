using System.Threading.Tasks;
using Expenses.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST
        [HttpPost("GetReport")]
        public async Task<ActionResult> GetReport(GetReportQuery request)
        {
            var report = await _mediator.Send(request);
            return Ok(report);
        }
    }
}