using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category()
            {
                Description = request.Description != null ? string.Empty: request.Description,
                Name = request.Name,
                UserId = request.UserId
            };

            await _unitOfWork.Categories.InsertAsync(category);
            await _unitOfWork.CommitAsync();
            return category.Id;
        }
    }
}