using System.Collections.Generic;
using Expenses.Domain.Models;
using MediatR;

namespace Expenses.API.Application.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public string UserId { get; set; }

        public GetCategoriesQuery(string userId)
        {
            UserId = userId;
        }
    }
}