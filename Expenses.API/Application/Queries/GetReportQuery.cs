using System;
using System.Collections.Generic;
using Expenses.Infrastructure.Dto;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetReportQuery : IRequest<IEnumerable<ReportDayDto>>
    {
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<int> SelectedAccounts { get; set; }
        public bool IncludeIncomes { get; set; }
    }
}