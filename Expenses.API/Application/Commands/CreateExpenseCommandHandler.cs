using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands
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
            // Id created by class itself.
            var expense = new Expense(request.Title, request.FullDate, request.CategoryId, request.UserId);
            await _unitOfWork.Expenses.InsertAsync(expense);
            await _unitOfWork.CommitAsync();
            return expense.Id;
        }
    }
}