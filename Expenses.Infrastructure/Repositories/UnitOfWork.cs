using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;

namespace Expenses.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpensesDbContext _expensesDbContext;
        private BaseRepository<Expense> _expenses;
        private BaseRepository<ExpenseDetail> _expenseDetails;

        public UnitOfWork(ExpensesDbContext expensesDbContext)
        {
            _expensesDbContext = expensesDbContext;
        }

        public IRepository<Expense> Expenses
        {
            get
            {
                return _expenses ??
                       (_expenses = new BaseRepository<Expense>(_expensesDbContext));
            }
            //todo not sure if this is OK
            set => throw new System.NotImplementedException();
        }

        public IRepository<ExpenseDetail> ExpenseDetails
        {
            get
            {
                return _expenseDetails ??
                       (_expenseDetails = new BaseRepository<ExpenseDetail>(_expensesDbContext));
            }
            set => throw new System.NotImplementedException();
        }

        public async Task CommitAsync()
        {
            await _expensesDbContext.SaveChangesAsync();
        }
    }
}