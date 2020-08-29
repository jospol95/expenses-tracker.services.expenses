using MediatR;

namespace Expenses.API.Application.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCategoryCommand(int id)
        {
            this.Id = id;
        }
    }
}