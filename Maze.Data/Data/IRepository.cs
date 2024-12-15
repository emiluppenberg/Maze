using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DataAccess.Data
{
    public interface IRepository<T> where T : class
    {
        T? Add(T entity);
        T? Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T? Get(int id);
        IEnumerable<T> All();
        void SaveChanges();
    }
}
