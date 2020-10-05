using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            if (request.IsConcurrent)
            {
                await AddConcurrentExpenses(request);
            }
            else
            {
                await AddSingleExpenseRequest(request);
            }
            await _unitOfWork.CommitAsync();
            
            return Unit.Value;
        }
        
        private async Task AddSingleExpenseRequest(CreateExpenseCommand request)
        {
            var expense = new Expense(
                Guid.NewGuid().ToString(), 
                request.Title, 
                request.Amount,
                request.Date, 
                request.UserId,
                request.Description,
                request.CategoryId,
                request.AccountId
            );
               
            await _unitOfWork.Expenses.InsertAsync(expense);

        }
        
        private async Task AddConcurrentExpenses(CreateExpenseCommand request)
        {
            var dateToStart = request.Date;
            for (var concurrentMonth = dateToStart.Month; concurrentMonth <= 12; concurrentMonth++)
            {
                try
                {
                    var expenseRequestToAdd = request;
                    expenseRequestToAdd.Date = new DateTime(request.Date.Year, concurrentMonth, request.Date.Day);
                    await AddSingleExpenseRequest(expenseRequestToAdd);
                    
                }
                catch (ArgumentOutOfRangeException invalidDateException)
                {
                    //if it's an invalid date, just skip it
                }
            }
        }
    }
}