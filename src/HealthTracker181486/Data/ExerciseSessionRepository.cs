using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class ExerciseSessionRepository : Repository<ExerciseSession>
    {
        public ExerciseSessionRepository(HealthDatabase database) : base(database)
        {
        }

        public override async Task<int> InsertAsync(ExerciseSession session)
        {
            var db = await GetConnectionAsync();
            var type = await db.FindAsync<ExerciseType>(session.ExerciseTypeId);

            if (type is null)
            {
                throw new InvalidOperationException($"ExerciseType with Id {session.ExerciseTypeId} does not exist.");
            }

            return await base.InsertAsync(session);
        }

        public async Task<List<ExerciseSession>> GetRecentAsync(int userId, int count)
        {
            var db = await GetConnectionAsync();
            return await db.Table<ExerciseSession>().Where(s => s.UserId == userId).OrderByDescending(s => s.StartTime).Take(count).ToListAsync();
        }
    }
}
