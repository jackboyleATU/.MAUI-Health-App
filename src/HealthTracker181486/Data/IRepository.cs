using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HealthTracker181486.Data
{
    public interface IRepository<T> where T : new()
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<int> InsertAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);

    }
}
