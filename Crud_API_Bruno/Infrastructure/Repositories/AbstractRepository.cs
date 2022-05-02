using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Crud_API_Bruno.Domain;
using Crud_API_Bruno.Infrastructure.Context;

namespace Crud_API_Bruno.Infrastructure.Repositories
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MainContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected AbstractRepository(MainContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        public void Dispose()
        {
            Db.Dispose();
        }
        public virtual TEntity Add(TEntity obj)
        {
            DbSet.Add(obj);

            return obj;
        }

        public virtual TEntity Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;

            return DbSet.Update(obj).Entity;
        }

        public virtual void Delete(int id)
        {
            var obj = FindById(id);

            DbSet.Remove(obj);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual TEntity FindById(int id)
        {
            return DbSet.Find(id);
        }
        public virtual IQueryable<TEntity> Query()
        {
            return DbSet.AsNoTracking();
        }

        public bool Commit()
        {
            return Db.SaveChanges() > 0;
        }
    }
}