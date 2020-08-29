using System;
using System.Collections.Generic;
using Expenses.Domain.Models;
using Expenses.Infrastructure.Application.Enums;

namespace Expenses.Infrastructure.Dto
{
    public class BudgetReportDto
    {
        public IEnumerable<Income> Incomes { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }

    }
}