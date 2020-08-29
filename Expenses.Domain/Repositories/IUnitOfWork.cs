using System.Threading.Tasks;
using Expenses.Domain.Models;

namespace Expenses.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Expense> Expenses { get; }
        IRepository<Income> Incomes { get; }
        IRepository<Category> Categories { get; }
        IRepository<Account> Accounts { get; }
        // IRepository<ExpenseDetail> ExpenseDetails { get; set; }
        Task CommitAsync();
    }
}