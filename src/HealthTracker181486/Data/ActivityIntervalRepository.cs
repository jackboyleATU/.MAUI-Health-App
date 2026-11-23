using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class ActivityIntervalRepository : Repository<ActivityInterval>
    {
        public ActivityIntervalRepository(HealthDatabase database) : base(database) { }

        public async Task<List<ActivityInterval>> GetBetweenAsync(int userId, DateTime fromUtc, DateTime toUtc)
        {
            var db = await GetConnectionAsync();
            return await db.Table<ActivityInterval>().Where(a => a.UserId == userId && a.IntervalStart >= fromUtc && a.IntervalStart < toUtc).OrderBy(a => a.IntervalStart).ToListAsync();
        }
    }
}
