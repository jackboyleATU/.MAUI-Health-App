using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class GoalRepository : Repository<Goal>
    {
        public GoalRepository(HealthDatabase database) : base(database) { }

        public override async Task<int> InsertAsync(Goal goal)
        {
            var db = await GetConnectionAsync();
            if (await db.FindAsync<GoalType>(goal.GoalTypeId) is null)
                throw new InvalidOperationException($"GoalType with Id {goal.GoalTypeId} does not exist");

            return await base.InsertAsync(goal);
        }

        public async Task<List<Goal>> GetActiveAsync(int userId)
        {
            var db = await GetConnectionAsync();
            return await db.Table<Goal>().Where(g => g.UserId == userId && g.IsActive).ToListAsync();
        }

        public async Task SetGoalAsync(int userId, int goalTypeId, int targetValue, bool reminderEnabled)
        {
            var db = await GetConnectionAsync();
            if (await db.FindAsync<GoalType>(goalTypeId) is null)
                throw new InvalidOperationException($"GoalType with Id {goalTypeId} does not exist");

            await db.RunInTransactionAsync(conn =>
            {
                var current = conn.Table<Goal>().Where(g => g.UserId == userId && g.GoalTypeId == goalTypeId && g.IsActive).ToList();

                foreach (var old in current)
                {
                    old.IsActive = false;
                    conn.Update(old);
                }

                conn.Insert(new Goal
                {
                    UserId = userId,
                    GoalTypeId = goalTypeId,
                    TargetValue = targetValue,
                    ReminderEnabled = reminderEnabled
                });
            });
        }
    }
}
