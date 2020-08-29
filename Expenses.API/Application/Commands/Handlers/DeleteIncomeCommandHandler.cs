using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIncomeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.Incomes.GetById(request.Id);
            if (income == null) return false;

            _unitOfWork.Incomes.Delete(income);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}