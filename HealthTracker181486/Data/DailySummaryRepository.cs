using HealthTracker181486.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker181486.Data
{
    public class DailySummaryRepository : Repository<DailySummary>
    {
        public DailySummaryRepository(HealthDatabase database) : base(database)
        {
        }

        public async Task<DailySummary?> GetForDateAsync(int userId, DateTime date)
        {
            var day = date.Date;
            var db = await GetConnectionAsync();
            return await db.Table<DailySummary>().Where(s => s.UserId == userId && s.Date == day).FirstOrDefaultAsync();
        }

        public async Task<List<DailySummary>> GetRangeAsync(int userId, DateTime from, DateTime to)
        {
            var start = from.Date;
            var end = to.Date;
            var db = await GetConnectionAsync();
            return await db.Table<DailySummary>().Where(s => s.UserId == userId && s.Date >= start && s.Date <= end).OrderBy(s => s.Date).ToListAsync();
        }

        public async Task SaveAsync(DailySummary summary)
        {
            summary.Date = summary.Date.Date;
            var existing = await GetForDateAsync(summary.UserId, summary.Date);

            if (existing is null)
            {
                await InsertAsync(summary);
            }
            else
            {
                summary.Id = existing.Id;
                await UpdateAsync(summary);
            }
        }
    }
}
