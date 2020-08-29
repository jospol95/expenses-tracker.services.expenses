using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Queries.Handlers
{
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery,Expense>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExpenseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Expense> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _unitOfWork.Expenses.GetById(request.Id);
            return expense;
        }
    }
}