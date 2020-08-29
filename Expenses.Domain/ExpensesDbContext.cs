using Expenses.Domain.KeylessModels;
using Expenses.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Domain
{
    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
        { }
        
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }  
        public DbSet<Category> Categories { get; set; }  
        public DbSet<Account> Accounts { get; set; }  

       
        //Keyless
        public DbSet<BudgetReport> BudgetReports { get; set; }
    }
}