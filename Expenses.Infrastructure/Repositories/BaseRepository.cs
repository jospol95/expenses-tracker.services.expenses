using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Domain.Repositories;
using Expenses.Domain;
using Microsoft.EntityFrameworkCore;


namespace Expenses.Infrastructure.Repositories
{
    public class BaseRepository <TEntity> : IRepository <TEntity> where TEntity : class
    {
        //todo prob wont work
        private readonly ExpensesDbContext _context;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(ExpensesDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<TEntity>();
        }
        
        public virtual async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            return entity;
        }
        
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }
    }
}