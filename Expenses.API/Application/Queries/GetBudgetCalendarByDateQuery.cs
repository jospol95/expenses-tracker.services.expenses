using System.Collections;
using System.Collections.Generic;
using Expenses.Domain.Models;
using Expenses.Infrastructure.Dto;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetBudgetCalendarByDateQuery : IRequest<IEnumerable<BudgetDay>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }

        public GetBudgetCalendarByDateQuery(int month, int year, int userId)
        {
            Month = month;
            Year = year;
            UserId = userId;
        }
    }
}