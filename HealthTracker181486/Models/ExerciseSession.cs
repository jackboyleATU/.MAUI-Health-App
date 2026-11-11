using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    [Table("ExerciseSession")]
    public class ExerciseSession
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public int ExerciseTypeId { get; set; }

        [Indexed]
        public DateTime StartTime { get; set; }
        public int DurationSeconds { get; set; }

        public double? DistanceKm { get; set; }
        public int? Steps { get; set; }
        public int? CaloriesBurned { get; set; }
    }
}
