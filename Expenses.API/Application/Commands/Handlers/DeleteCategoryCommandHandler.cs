using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetById(request.Id);

            if (category == null) return false;
            
            var userExpensesForCategory = _unitOfWork.Expenses.Get()
                .Where(exp => exp.UserId == request.UserId)
                .Where(exp => exp.CategoryId == request.Id);

            foreach (var expense in userExpensesForCategory)
            {
                expense.DeleteCategory();
            }
            
            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.CommitAsync();
            
            return true;
        }
    }
}