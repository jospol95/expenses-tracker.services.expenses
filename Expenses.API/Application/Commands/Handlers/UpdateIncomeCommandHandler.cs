using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIncomeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.Incomes.GetById(request.Id);
            income.Title = request.Title;
            income.Amount = request.Amount;
            income.Description = request.Description;
            income.AccountId = request.AccountId;
            
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}