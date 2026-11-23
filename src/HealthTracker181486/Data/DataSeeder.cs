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

            var stepsType = new GoalType { Name = "Steps", Unit = "steps", MinTarget=1000, MaxTarget=50000, DefaultTarget=6000 };
            var caloriesType = new GoalType { Name = "Calories", Unit = "calories", MinTarget = 100, MaxTarget = 3000, DefaultTarget = 600 };
            var sleepType = new GoalType { Name = "Sleep", Unit = "minutes", MinTarget = 60, MaxTarget = 720, DefaultTarget = 420 };
            await db.InsertAllAsync(new[] { stepsType, caloriesType, sleepType });

            await db.InsertAllAsync(new[]
            {
                new MeasurementType {Name = "Heart rate", Unit = "bpm"},
                new MeasurementType {Name = "Blood oxygen", Unit = "%"},
            });

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
                new Goal { UserId = user.Id, GoalTypeId = stepsType.Id, TargetValue = stepsType.DefaultTarget},
                new Goal { UserId = user.Id, GoalTypeId = caloriesType.Id, TargetValue = caloriesType.DefaultTarget},
                new Goal { UserId = user.Id, GoalTypeId = sleepType.Id, TargetValue = sleepType.DefaultTarget},
            });

            await SampleDataGenerator.GenerateAsync(db, user.Id);
        }
    }
}
