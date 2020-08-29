using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetExpenseByIdQuery : IRequest<Expense>
    {
        public string Id { get; set; }
        public GetExpenseByIdQuery(string id)
        {
            Id = id;
        }
    }
}