using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Expenses.API.Application.Queries;
using Expenses.Domain;
using Expenses.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery,IEnumerable<Account>>
    {
        private readonly ExpensesDbContext _dbContext;

        public GetAccountsQueryHandler(ExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Account>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await
                _dbContext.Accounts.FromSqlRaw("SELECT * FROM dbo.Account a WHERE a.user_id = {0}", request.UserId)
                    .ToListAsync(cancellationToken);
            
            return accounts;
        }
    }
}