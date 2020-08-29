using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetIncomeByIdQuery : IRequest<Income>
    {
        public string Id { get; set; }

        public GetIncomeByIdQuery(string id)
        {
            Id = id;
        }
    }
}