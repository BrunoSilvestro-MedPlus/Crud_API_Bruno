using System;
using System.Linq;

namespace Crud_API_Bruno.Domain
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(int id);
        void Remove(TEntity entity);
        TEntity FindById(int id);
        IQueryable<TEntity> Query();
        bool Commit();
    }
}