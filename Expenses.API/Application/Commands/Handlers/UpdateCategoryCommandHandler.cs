using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var dbCategory = await _unitOfWork.Categories.GetById(request.Id);

            dbCategory.Description = request.Description;
            dbCategory.Name = request.Name;

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}