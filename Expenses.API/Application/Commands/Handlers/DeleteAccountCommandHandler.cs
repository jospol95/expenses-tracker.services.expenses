using System.Threading;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using MediatR;

namespace Expenses.API.Application.Commands.Handlers
{
    public class DeleteAccountCommandHandler: IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var dbAccount = await _unitOfWork.Accounts.GetById(request.Id);
            if (dbAccount == null) return false;

            
            _unitOfWork.Accounts.Delete(dbAccount);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}