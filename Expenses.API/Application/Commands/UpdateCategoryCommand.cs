using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Commands
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}