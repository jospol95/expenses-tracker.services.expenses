using System.Threading.Tasks;
using Expenses.Domain.Models;

namespace Expenses.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Expense> Expenses { get; set; }
        IRepository<ExpenseDetail> ExpenseDetails { get; set; }
        Task CommitAsync();
    }
}