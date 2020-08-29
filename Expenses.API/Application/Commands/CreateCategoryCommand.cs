using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}