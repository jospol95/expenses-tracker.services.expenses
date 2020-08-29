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
        // private BaseRepository<ExpenseDetail> _expenseDetails;
        private BaseRepository<Income> _incomes;
        private BaseRepository<Category> _categories;
        private BaseRepository<Account> _accounts;

        public UnitOfWork(ExpensesDbContext expensesDbContext)
        {
            _expensesDbContext = expensesDbContext;
        }

        public IRepository<Category> Categories
        {
            get
            {
                return _categories ??
                       (_categories = new BaseRepository<Category>(_expensesDbContext));
            }
        }
        
        public IRepository<Account> Accounts
        {
            get
            {
                return _accounts ??
                       (_accounts = new BaseRepository<Account>(_expensesDbContext));
            }
        }

        public IRepository<Expense> Expenses
        {
            get
            {
                return _expenses ??
                       (_expenses = new BaseRepository<Expense>(_expensesDbContext));
            }
            //todo not sure if this is OK
            // set => throw new System.NotImplementedException();
        }

        public IRepository<Income> Incomes
        {
            get
            {
                return _incomes ??
                       (_incomes = new BaseRepository<Income>(_expensesDbContext));
            }
        }

        public async Task CommitAsync()
        {
            await _expensesDbContext.SaveChangesAsync();
        }
    }
}