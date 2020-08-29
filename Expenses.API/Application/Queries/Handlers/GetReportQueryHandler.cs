using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Infrastructure.Application.Enums;
using Expenses.Infrastructure.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, IEnumerable<ReportDayDto>>
    {
        private readonly ExpensesDbContext _dbContext;

        public GetReportQueryHandler(ExpensesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<ReportDayDto>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            // var accountsList = string.Join(",", request.SelectedAccounts);
            // var categoryList = string.Join(",", request.SelectedCategories);
            
            //needs to return an ordered list per day
            var expensesList = _dbContext.Expenses
                .FromSqlRaw("SELECT * " +
                            "FROM dbo.expense E where " +
                            "E.date BETWEEN  {0} AND {1} ",
                    request.StartDate.Date, request.EndDate.Date);

            var incomesList = _dbContext.Incomes
                .FromSqlRaw("SELECT * " +
                            "FROM dbo.income I where " +
                            "I.date BETWEEN  {0} AND {1} ",
                    request.StartDate.Date, request.EndDate.Date);

            var filteredIncomesList = incomesList
                .Where(income => (income.AccountId != null && request.SelectedAccounts.Contains(income.AccountId.Value)))
                .ToList();
            var filteredExpensesList = expensesList
                .Where(expense => (expense.AccountId != null && (request.SelectedAccounts.Contains(expense.AccountId.Value))) &&
                                  (expense.CategoryId != null &&request.SelectedCategories.Contains(expense.CategoryId.Value)))
                .ToList();

            var orderedReport = new List<ReportDayDto>();
            for (var i = request.StartDate; i < request.EndDate; i = i.AddDays(1))
            {
                var currentDate = i.Date;

                var incomesForThatDay = filteredIncomesList.Where(income => income.Date.Date == currentDate);
                var expensesForThatDay = filteredExpensesList.Where(exp => exp.Date.Date == currentDate);
                
                foreach (var income in incomesForThatDay)
                {
                    orderedReport.Add(new ReportDayDto()
                    {
                        Date = income.Date,
                        AccountId = income.AccountId,
                        Amount = income.Amount,
                        Description = income.Title,
                        BudgetRecordType = BudgetRecordType.Income
                    });
                }
                
                foreach (var expense in expensesForThatDay)
                {
                    orderedReport.Add(new ReportDayDto()
                        {
                            Date = expense.Date,
                            AccountId = expense.AccountId,
                            Amount = expense.Amount,
                            CategoryId = expense.CategoryId,
                            Description = expense.Title,
                            BudgetRecordType = BudgetRecordType.Expense
                        });
                }
                
            }
            
            return orderedReport;

        }
    }
}