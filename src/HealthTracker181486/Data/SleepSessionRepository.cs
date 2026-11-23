using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class SleepSessionRepository : Repository<SleepSession>
    {
        public SleepSessionRepository(HealthDatabase database) : base(database) { }

        public async Task<List<SleepSession>> GetEndingBetweenAsync(int userId, DateTime fromUtc, DateTime toUtc)
        {
            var db = await GetConnectionAsync();
            return await db.Table<SleepSession>().Where(s => s.UserId == userId && s.EndTime >= fromUtc && s.EndTime < toUtc).OrderBy(s => s.StartTime).ToListAsync();
        }
    }
}
