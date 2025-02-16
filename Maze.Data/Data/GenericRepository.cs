using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DataAccess.Data
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        public MazeDbContext context;
        public GenericRepository(MazeDbContext context)
        {
            this.context = context;
        }

        public virtual T? Add(T entity)
        {
            if (entity != null)
            {
                context.Add(entity);
                return entity;
            }

            return null;
        }

        public virtual IEnumerable<T> All()
        {
            return context.Set<T>().ToList();
        }

        public virtual void Delete(T entity)
        {
            if (entity != null)
            {
                context.Remove(entity);
            }
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual T? Get(int id)
        {
            return context.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual T? Update(T entity)
        {
            return context.Update(entity).Entity;
        }
    }
}
