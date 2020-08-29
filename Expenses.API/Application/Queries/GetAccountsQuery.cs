using System.Collections.Generic;
using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public string UserId { get; set; }

        public GetAccountsQuery(string userId)
        {
            UserId = userId;
        }
    }
}