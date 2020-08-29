using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _unitOfWork.Expenses.GetById(request.Id);
            if (expense == null) return false;

            _unitOfWork.Expenses.Delete(expense);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}