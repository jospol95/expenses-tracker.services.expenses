using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using Expenses.Infrastructure.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetBudgetCalendarByDateQueryHandler : IRequestHandler<GetBudgetCalendarByDateQuery, IEnumerable<BudgetDay>>
    {
        private readonly ExpensesDbContext _dbContext;

        public GetBudgetCalendarByDateQueryHandler(ExpensesDbContext dbContext)
        {
            //Use Dapper!!!
            // or just a rawSql
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<BudgetDay>> Handle(GetBudgetCalendarByDateQuery request, CancellationToken cancellationToken)
        {
            if (isDateProvidedValid(request.Month)) return null;
            request.Month++;
            //todo Add UserId to param
            // getting the list of expenses and incomes for that month in that year
            var expensesListForMonth = _dbContext.Expenses
                .FromSqlRaw("SELECT * " +
                            "FROM dbo.expense EX where " +
                            "MONTH(EX.DATE) = {0} AND YEAR(EX.DATE) = {1}",
                    request.Month, request.Year
                );
            
            var incomeListForMonth = _dbContext.Incomes
                .FromSqlRaw("SELECT * " +
                            "FROM dbo.income INC where " +
                            "MONTH(INC.DATE) = {0} AND YEAR(INC.DATE) = {1}",
                    request.Month, request.Year
                );

            var budgetForMonth = GetBudgetDateArray(
                request.Month,
                request.Year,
                expensesListForMonth.ToList().AsReadOnly(),
                incomeListForMonth.ToList().AsReadOnly()
            );
            
            return budgetForMonth;
        }

        private bool isDateProvidedValid(int month)
        {
            return month >= 1 && month < 1;

        }

        private IEnumerable<BudgetDay> GetBudgetDateArray(int month, int year, IReadOnlyCollection<Expense> expensesForMonth, IReadOnlyCollection<Income> incomeListForMonth)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var budgetListForMonth = new List<BudgetDay>();
            
            for(var i = 1; i <= daysInMonth; i++)
            {
                var day = i;
                var budgetDay = new BudgetDay(new DateTime(year, month, day))
                {
                    Expenses = expensesForMonth.Where(e => e.Date.Day.Equals(day)),
                    Incomes = incomeListForMonth.Where(e => e.Date.Day.Equals(day))
                };
                budgetListForMonth.Add(budgetDay);
            }

            return budgetListForMonth;
        }
    }
}