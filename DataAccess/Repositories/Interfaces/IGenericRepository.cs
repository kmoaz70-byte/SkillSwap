using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll( string? includeProperties = null);

        T? GetOne(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T obj);

        void Remove(T obj);

        void RemoveRange(IEnumerable<T> obj);
    }
}
