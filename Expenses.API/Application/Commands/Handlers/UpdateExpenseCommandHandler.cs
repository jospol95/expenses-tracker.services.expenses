using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _unitOfWork.Expenses.GetById(request.Id);
            expense.Title = request.Title;
            expense.Amount = request.Amount;
            expense.Description = request.Description;
            expense.CategoryId = request.CategoryId;
            expense.AccountId = request.AccountId;
            
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}