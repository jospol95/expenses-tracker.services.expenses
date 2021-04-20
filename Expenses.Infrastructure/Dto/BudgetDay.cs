using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Expenses.Domain.Models;

namespace Expenses.Infrastructure.Dto
{
    public class BudgetDay
    {
        public DateTime FullDate { get; set; }
        public int Day{ get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Income> Incomes { get; set; }

        public int ExpensesTotal;
        public int IncomesTotal;

        public BudgetDay(DateTime fullDate)
        {
            FullDate = fullDate;
            Day = fullDate.Day;
            Month = fullDate.Month;
            Year = fullDate.Year;
        }

        public BudgetDay()
        {
            
        }
        
        public IEnumerable<BudgetDay> ConvertExpenseAndIncomeListToDTO(int month, int year, IReadOnlyCollection<Expense> expensesForMonth, IReadOnlyCollection<Income> incomeListForMonth)
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