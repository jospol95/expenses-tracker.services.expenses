using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}