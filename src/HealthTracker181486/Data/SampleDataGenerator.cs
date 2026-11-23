using System;
using System.Collections.Generic;
using System.Text;
using HealthTracker181486.Models;
using SQLite;

namespace HealthTracker181486.Data
{
    public static class SampleDataGenerator
    {
        const int SlotMinutes = 30;
        const int SlotsPerDay = 24 * 60 / SlotMinutes;
        const int HeartRateEveryMinutes = 10;

        public static async Task GenerateAsync(SQLiteAsyncConnection db, int userId, int days = 90, int seed = 42)
        {
            var random = new Random(seed);

            var user = await db.GetAsync<User>(userId);
            var exerciseTypes = await db.Table<ExerciseType>().ToListAsync();
            var heartRateType = await db.Table<MeasurementType>().Where(t => t.Name == "Heart rate").FirstOrDefaultAsync() ?? throw new InvalidOperationException("The 'Heart rate' measurement type has not been seeded.");

            double strideKm = user.HeightCm * 0.415 / 100_000;

            var now = DateTime.Now;
            var firstDay = DateTime.Today.AddDays(-(days - 1));

            //1. Sleep
            var sleepTimes = new List<(DateTime Start, DateTime End)>();
            for (int d = 0; d <= days; d++)
            {
                var night = firstDay.AddDays(d - 1);
                var start = night.AddHours(22.5).AddMinutes(random.Next(0, 120));
                var end = start.AddMinutes(random.Next(360, 541));
                if (end <= now)
                    sleepTimes.Add((start, end));
            }

            var sleeps = sleepTimes.Select(s => new SleepSession
            {
                UserId = userId,
                StartTime = s.Start.ToUniversalTime(),
                EndTime = s.End.ToUniversalTime(),
                Source = DataSource.Wearable
            }).ToList();

            var sessions = new List<ExerciseSession>();
            var intervals = new List<ActivityInterval>();
            var heartRates = new List<HealthMeasurement>();

            for (int d = 0; d < days; d++)
            {
                var day = firstDay.AddDays(d);
                bool weekend = day.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
                double activityLevel = 0.6 + random.NextDouble() * 0.8;

                // 2. Exercise
                ExerciseType? exerciseType = null;
                DateTime exStart = default, exEnd = default;

                if (exerciseTypes.Count > 0 && random.NextDouble() < 0.45)
                {
                    var type = exerciseTypes[random.Next(exerciseTypes.Count)];
                    var start = day.AddHours(weekend ? 10 : 18).AddMinutes(random.Next(0, 90));
                    int minutes = random.Next(20, 76);
                    var end = start.AddMinutes(minutes);

                    if (end <= now)
                    {
                        exerciseType = type;
                        exStart = start;
                        exEnd = end;

                        double hours = minutes / 60.0;
                        int stepsPerMinute = StepsPerMinute(type.Name);

                        sessions.Add(new ExerciseSession
                        {
                            UserId = userId,
                            ExerciseTypeId = type.Id,
                            StartTime = start.ToUniversalTime(),
                            DurationSeconds = minutes * 60,
                            DistanceKm = type.TracksDistance ? Math.Round(SpeedKmh(type.Name) * hours, 2) : null,
                            Steps = stepsPerMinute > 0 ? stepsPerMinute * minutes : null,
                            CaloriesBurned = (int)Math.Round(type.MetValue * user.WeightKg * hours),
                            Source = DataSource.Wearable
                        });
                    }
                }

                // 3. Activity
                for (int slot = 0; slot < SlotsPerDay; slot++)
                {
                    var slotStart = day.AddMinutes(slot * SlotMinutes);
                    if (slotStart >= now) break;
                    var slotEnd = slotStart.AddMinutes(SlotMinutes);

                    bool asleep = sleepTimes.Any(s => slotStart >= s.Start && slotStart < s.End);

                    int steps = asleep ? 0 :
                        (int)(BaseSteps(slotStart.TimeOfDay.TotalHours, weekend) * activityLevel * (0.6 + random.NextDouble() * 0.8));

                    int calories = (int)Math.Round(steps * 0.04);
                    double exerciseIntensity = 0;

                    if (exerciseType is not null)
                    {
                        double overlap = OverlapMinutes(slotStart, slotEnd, exStart, exEnd);
                        if (overlap > 0)
                        {
                            steps += (int)(StepsPerMinute(exerciseType.Name) * overlap);
                            calories += (int)Math.Round(exerciseType.MetValue * user.WeightKg * overlap / 60.0);
                            exerciseIntensity = exerciseType.MetValue;
                        }
                    }

                    intervals.Add(new ActivityInterval
                    {
                        UserId = userId,
                        IntervalStart = slotStart.ToUniversalTime(),
                        Steps = steps,
                        CaloriesBurned = calories,
                        DistanceKm = Math.Round(steps * strideKm, 3),
                        Floors = steps > 200 && random.NextDouble() < 0.25 ? random.Next(1, 3) : 0
                    });

                    // 4. Heart Rate
                    for (int m = 0; m < SlotMinutes; m  += HeartRateEveryMinutes)
                    {
                        var readingTime = slotStart.AddMinutes(m);
                        if (readingTime >= now) break;

                        double bpm;
                        if (asleep)
                            bpm = 50 + random.Next(0, 9);
                        else if (exerciseIntensity > 0)
                            bpm = Math.Min(185, 95 + exerciseIntensity * 7 + random.Next(-8, 9));
                        else
                            bpm = 62 + random.Next(0, 14) + Math.Min(25, steps / 30.0);

                        heartRates.Add(new HealthMeasurement
                        {
                            UserId = userId,
                            MeasurementTypeId = heartRateType.Id,
                            Value = Math.Round(bpm),
                            RecordedAt = readingTime.ToUniversalTime(),
                            Source = DataSource.Wearable
                        });
                    }
                }
            }

            await db.InsertAllAsync(sleeps);
            await db.InsertAllAsync(sessions);
            await db.InsertAllAsync(intervals);
            await db.InsertAllAsync(heartRates);
        }

        static double BaseSteps(double hour, bool weekend)
        {
            if (weekend)
                return hour switch
                {
                    < 9 => 30,
                    < 12 => 300,
                    < 18 => 280,
                    < 22 => 180,
                    _ => 30
                };

            return hour switch
            {
                < 7 => 20,
                < 9 => 400,
                < 12 => 120,
                < 14 => 350,
                < 17 => 120,
                < 19 => 350,
                < 22.5 => 150,
                _ => 30
            };
        }

        static int StepsPerMinute(string exercise) => exercise switch
        {
            "Walking" => 110,
            "Running" => 165,
            "Football" => 90,
            _ => 0
        };

        static double SpeedKmh(string exercise) => exercise switch
        {
            "Walking" => 5,
            "Running" => 10,
            "Cycling" => 20,
            _ => 0
        };

        static double OverlapMinutes(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            var start = aStart > bStart ? aStart : bStart;
            var end = aEnd < bEnd ? aEnd : bEnd;
            return end > start ? (end - start).TotalMinutes : 0;
        }
    }
}
