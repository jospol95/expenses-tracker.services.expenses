using MediatR;

namespace Expenses.API.Application.Commands
{
    public class UpdateIncomeCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Title{get;set;}
        public decimal Amount{get;set;}
        public string Description{get;set;}
        public int? AccountId { get; set; }

    }
}