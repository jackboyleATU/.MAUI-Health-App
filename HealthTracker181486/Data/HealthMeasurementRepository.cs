using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class HealthMeasurementRepository : Repository<HealthMeasurement>
    {
        public HealthMeasurementRepository(HealthDatabase database) : base(database)
        {
        }

        public async Task<List<HealthMeasurement>> GetBetweenAsync(int userId, MeasurementType type, DateTime fromUtc, DateTime toUtc)
        {
            var db = await GetConnectionAsync();
            return await db.Table<HealthMeasurement>().Where(m => m.UserId == userId && m.Type == type && m.RecordedAt >= fromUtc && m.RecordedAt < toUtc).OrderBy(m => m.RecordedAt).ToListAsync();
        }

    }
}
