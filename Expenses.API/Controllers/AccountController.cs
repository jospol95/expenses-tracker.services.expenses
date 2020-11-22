using System.Threading.Tasks;
using Expenses.API.Application.Commands;
using Expenses.API.Application.Queries;
using Expenses.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAccountsForUser(string userId)
        {
            var query = new GetAccountsQuery(userId);
            var accounts = await _mediator.Send(query);
            if (accounts == null) return NotFound();

            else return Ok(accounts);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAccountCommand account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var accountId = await _mediator.Send(account);
            return Ok(accountId);
        }
        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountCommand account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            var foundAndUpdated = await _mediator.Send(account);
            if (!foundAndUpdated) return NotFound();
            
            return NoContent();
        } 
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var deleteAccountCommand = new DeleteAccountCommand(id);
            var foundAndDeleted = await _mediator.Send(deleteAccountCommand);
            if (!foundAndDeleted)  return NotFound();

            return Ok();
        } 
    }
}