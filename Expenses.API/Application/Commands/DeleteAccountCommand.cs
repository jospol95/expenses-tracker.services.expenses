using MediatR;

namespace Expenses.API.Application.Commands
{
    public class DeleteAccountCommand: IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAccountCommand(int id)
        {
            Id = id;
        }
    }
}