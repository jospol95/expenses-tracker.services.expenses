using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Infrastructure.Dto;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetBudgetSummaryQuery : IRequest<IEnumerable<BudgetDay>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }

        public GetBudgetSummaryQuery(int month, int year, string userId)
        {
            Month = month;
            Year = year;
            UserId = userId;
        }

    }

    public class GetBudgetSummaryForMonthQueryHandler : IRequestHandler<GetBudgetSummaryQuery, IEnumerable<BudgetDay>>
    {
        private readonly ExpensesDbContext _dbContext;
        public GetBudgetSummaryForMonthQueryHandler(ExpensesDbContext dbContext)
        {
            //Use Dapper!!!
            // or just a rawSql
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<BudgetDay>> Handle(GetBudgetSummaryQuery request, CancellationToken cancellationToken)
        {
            request.Month++;
            var expenses = _dbContext.Expenses.Where(e =>
                e.Date.Month == request.Month 
                && e.Date.Year == request.Year 
                && e.UserId == request.UserId);
            
            var incomes = _dbContext.Incomes.Where(e =>
                e.Date.Month == request.Month 
                && e.Date.Year == request.Year 
                && e.UserId == request.UserId);
            
            var budgetDaySummary = new BudgetDay()
                .ConvertExpenseAndIncomeListToDTO(request.Month, 
                    request.Year, 
                    expenses.ToList().AsReadOnly(), 
                    incomes.ToList().AsReadOnly());

            return budgetDaySummary;
        }
    }
}