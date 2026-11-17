using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;
using SQLite;

namespace HealthTracker181486.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(SQLiteAsyncConnection db)
        {
            if (await db.Table<User>().CountAsync() > 0)
                return;

            var user = new User
            {
                Name = "John Smith",
                Email = "john.smith@gmail.com",
                DateOfBirth = new DateTime(2004, 10, 19),
                Sex = Sex.Male,
                HeightCm = 180,
                WeightKg = 75
            };
            await db.InsertAsync(user);

            await db.InsertAllAsync(new[]
            {
                new ExerciseType { Name = "Walking", IconValue = "walking.png", MetValue = 3.5, TracksDistance = true},
                new ExerciseType { Name = "Running", IconValue = "running.png", MetValue = 9.8, TracksDistance = true},
                new ExerciseType { Name = "Cycling", IconValue = "cycling.png", MetValue = 7.5, TracksDistance = true},
                new ExerciseType { Name = "Football", IconValue = "football.png", MetValue = 7.0, TracksDistance = false},
                new ExerciseType { Name = "Workout", IconValue = "workout.png", MetValue = 5.0, TracksDistance = false}
            });

            await db.InsertAllAsync(new[]
            {
                new Goal { UserId = user.Id, GoalType = GoalType.Steps, TargetValue = 6000},
                new Goal { UserId = user.Id, GoalType = GoalType.Calories, TargetValue = 600},
                new Goal { UserId = user.Id, GoalType = GoalType.Sleep, TargetValue = 420}, // minutes (7h)
            });
        }
    }
}
