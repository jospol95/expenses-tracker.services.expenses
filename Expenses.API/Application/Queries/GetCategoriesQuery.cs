using System.Collections.Generic;
using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public int UserId { get; set; }

        public GetCategoriesQuery(int userId)
        {
            UserId = userId;
        }
    }
}