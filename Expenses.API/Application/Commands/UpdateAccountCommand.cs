using MediatR;

namespace Expenses.API.Application.Commands
{
    public class UpdateAccountCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}