using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIncomeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<string> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = new Income(Guid.NewGuid().ToString(), request.Title, request.Amount,
                request.Date, request.UserId, request.Description, request.AccountId);
            
            await _unitOfWork.Incomes.InsertAsync(income);
            await _unitOfWork.CommitAsync();

            return income.Id;
        }
    }
}