using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    [Table("DailySummary")]
    public class DailySummary
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "UX_DailySummary_UserDate", Order = 1, Unique = true)]
        public int UserId { get; set; }

        [Indexed(Name = "UX_DailySummary_UserDate", Order = 2, Unique = true)]
        public DateTime Date { get; set; }

        public int Steps { get; set; }
        public double DistanceKm { get; set; }
        public int CaloriesBurned { get; set; }
        public int Floors { get; set; }
        public int SleepMinutes { get; set; }
        public int? RestingHeartRate { get; set; }
    }
}
