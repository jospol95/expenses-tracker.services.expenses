using MediatR;

namespace Expenses.API.Application.Commands
{
    public class DeleteIncomeCommand : IRequest<bool>
    {
        public string Id { get; set; } 

        public DeleteIncomeCommand(string id)
        {
            Id = id;
        }
    }
}