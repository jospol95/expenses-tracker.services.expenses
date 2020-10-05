using System;
using MediatR;

namespace Expenses.API.Application.Commands
{
    public class CreateIncomeCommand : IRequest<Unit>
    {
        public string Title{get;set;}
        public decimal Amount{get;set;}
        public DateTime Date{get;set;}
        public string UserId{get;set;}    
        public string Description{get;set;}
        public int AccountId { get; set; }
        public bool IsConcurrent { get; set; }

    }
}