using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expenses.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entityToDelete);
        void DeleteById(object id);
        Task<TEntity> GetById(object id);
        //todo add filters
        IEnumerable<TEntity> Get();
        // IEnumerable<TEntity> GetWithPagination();
    }
}