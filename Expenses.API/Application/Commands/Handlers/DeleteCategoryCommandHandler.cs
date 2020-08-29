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
            if (category == null)
            {
                return false;
            }

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.CommitAsync();
            
            return true;
        }
    }
}