using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ExpensesDbContext _dbContext;

        public GetCategoriesQueryHandler(ExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoriesForUser = _dbContext.Categories
                .FromSqlRaw("SELECT * FROM dbo.category where user_id = {0} ORDER BY id DESC", request.UserId);

            return categoriesForUser;
        }
    }
}