using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker181486.Models
{
    public class DailySummary
    {
        public DateTime Date { get; init; }
        public int Steps { get; init; }
        public double DistanceKm { get; init; }
        public int CaloriesBurned { get; init; }
        public int Floors { get; init; }
        public int SleepMinutes { get; init; }
        public int? RestingHeartRate { get; init; }
    }
}
