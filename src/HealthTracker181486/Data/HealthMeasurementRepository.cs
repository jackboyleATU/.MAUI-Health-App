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

        public override async Task<int> InsertAsync(HealthMeasurement measurement)
        {
            var db = await GetConnectionAsync();
            if (await db.FindAsync<MeasurementType>(measurement.MeasurementTypeId) is null)
                throw new InvalidOperationException($"MeasurementType with Id {measurement.MeasurementTypeId} does not exist.");

            return await base.InsertAsync(measurement);
        }

        public async Task<List<HealthMeasurement>> GetBetweenAsync(int userId, int measurementTypeId, DateTime fromUtc, DateTime toUtc)
        {
            var db = await GetConnectionAsync();
            return await db.Table<HealthMeasurement>().Where(m => m.UserId == userId && m.MeasurementTypeId == measurementTypeId && m.RecordedAt >= fromUtc && m.RecordedAt < toUtc).OrderBy(m => m.RecordedAt).ToListAsync();
        }

    }
}
