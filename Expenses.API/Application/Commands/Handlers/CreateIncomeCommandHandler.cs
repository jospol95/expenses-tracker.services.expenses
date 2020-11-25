using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIncomeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            if (request.IsConcurrent)
            {
                await AddConcurrentIncomes(request);
            }
            else
            {
                await AddSingleIncomeRequest(request);
            }
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

        private async Task AddSingleIncomeRequest(CreateIncomeCommand request)
        {
            var income = new Income(Guid.NewGuid().ToString(), request.Title, request.Amount,
                request.Date, request.UserId, request.Description, request.AccountId);
            await _unitOfWork.Incomes.InsertAsync(income);

        }

        private async Task AddConcurrentIncomes(CreateIncomeCommand request)
        {
            var dateToStart = request.Date;
            for (var concurrentMonth = dateToStart.Month; concurrentMonth <= 12; concurrentMonth++)
            {
                try
                {
                    var incomeRequestToAdd = request;
                    incomeRequestToAdd.Date = new DateTime(request.Date.Year, concurrentMonth, request.Date.Day);
                    await AddSingleIncomeRequest(incomeRequestToAdd);
                    
                }
                catch (ArgumentOutOfRangeException invalidDateException)
                {
                    //if it's an invalid date, just skip it
                }
            }
        }
    }
}