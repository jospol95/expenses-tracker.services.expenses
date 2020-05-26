using System.Collections;
using System.Collections.Generic;

namespace Expenses.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Insert(object id);
        void Update(object id);
        void Delete(TEntity entityToDelete);
        void DeleteById(object id);
        TEntity GetById(TEntity entity);
        //todo add filters
        IEnumerable<TEntity> Get();
        // IEnumerable<TEntity> GetWithPagination();
    }
}