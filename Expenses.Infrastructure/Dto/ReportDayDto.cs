using System;
using Expenses.Infrastructure.Application.Enums;

namespace Expenses.Infrastructure.Dto
{
    public class ReportDayDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int? CategoryId { get; set; }
        public int? AccountId { get; set; }
        
        public BudgetRecordType BudgetRecordType { get; set; }
        
    }
}