using System.Collections.Generic;
using Expenses.Domain.KeylessModels;
using Expenses.Infrastructure.Application.Enums;
using Expenses.Infrastructure.Dto;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetBudgetReportQuery: IRequest<IEnumerable<BudgetReport>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }
        public BudgetReportEnum ReportType { get; set; }
        
    }
}