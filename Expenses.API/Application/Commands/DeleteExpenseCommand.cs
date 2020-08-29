using MediatR;

namespace Expenses.API.Application.Commands
{
    public class DeleteExpenseCommand : IRequest<bool>
    {
        public string Id { get; set; } 

        public DeleteExpenseCommand(string id)
        {
            Id = id;
        }
    }
}