using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SQLite;

namespace HealthTracker181486.Data
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        protected readonly HealthDatabase Database;

        public Repository(HealthDatabase database)
        {
            Database = database;
        }

        protected Task<SQLiteAsyncConnection> GetConnectionAsync() => Database.GetConnectionAsync();

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var db = await GetConnectionAsync();
            return await db.FindAsync<T>(id);
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            var db = await GetConnectionAsync();
            return await db.Table<T>().ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var db = await GetConnectionAsync();
            return await db.Table<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<int> InsertAsync(T item)
        {
            var db = await GetConnectionAsync();
            return await db.InsertAsync(item);
        }

        public virtual async Task<int> UpdateAsync(T item)
        {
            var db = await GetConnectionAsync();
            return await db.UpdateAsync(item);
        }

        public virtual async Task<int> DeleteAsync(T item)
        {
            var db = await GetConnectionAsync();
            return await db.DeleteAsync(item);
        }
    }
}
