using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account()
            {
                Description = request.Description != null ? string.Empty: request.Description,
                Name = request.Name,
                UserId = request.UserId
            };

            await _unitOfWork.Accounts.InsertAsync(account);
            await _unitOfWork.CommitAsync();
            return account.Id;
        }
    }
}