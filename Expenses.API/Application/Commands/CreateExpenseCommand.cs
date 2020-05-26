using System;
using MediatR;

namespace Expenses.API.Application.Commands
{
    public class CreateExpenseCommand: IRequest<string>
    {
        public string Title{get;set;}
        
        public DateTime FullDate{get;set;}
        
        public int CategoryId{get;set;}
        
        public string UserId{get;set;}
        
    }
}