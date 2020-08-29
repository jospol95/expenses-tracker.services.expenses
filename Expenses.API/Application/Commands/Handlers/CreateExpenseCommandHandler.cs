using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<string> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
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
            await _unitOfWork.CommitAsync();
            
            return expense.Id;
        }
    }
}