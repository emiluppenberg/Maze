using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DataAccess.Data
{
    abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        private MazeDbContext context;
        public GenericRepository(MazeDbContext context)
        {
            this.context = context;
        }

        public T? Add(T entity)
        {
            if (entity != null)
            {
                context.Add(entity);
                return entity;
            }

            return null;
        }

        public IEnumerable<T> All()
        {
            return context.Set<T>().ToList();
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                context.Remove(entity);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public T? Get(int id)
        {
            return context.Find<T>(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public T? Update(T entity)
        {
            return context.Update(entity).Entity;
        }
    }
}
