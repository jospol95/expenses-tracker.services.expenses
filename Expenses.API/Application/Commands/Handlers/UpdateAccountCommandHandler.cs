using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateAccountCommand account, CancellationToken cancellationToken)
        {
            var dbAccount = await _unitOfWork.Accounts.GetById(account.Id);
            if (dbAccount == null) return false;

            dbAccount.Update(account.Description, account.Name);
            // dbAccount.Description = account.Description;
            // dbAccount.Name = account.Name;
            await _unitOfWork.CommitAsync();
            
            return true;
        }
    }
}