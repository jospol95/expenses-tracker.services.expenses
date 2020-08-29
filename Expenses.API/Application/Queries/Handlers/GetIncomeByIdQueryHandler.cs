using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, Income>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetIncomeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Income> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.Incomes.GetById(request.Id);
            return income;
        }
    }
}