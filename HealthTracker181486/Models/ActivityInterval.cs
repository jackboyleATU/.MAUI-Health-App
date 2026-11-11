using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    [Table("ActivityInterval")]
    public class ActivityInterval
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public DateTime IntervalStart { get; set; }

        public int Steps { get; set; }

        public int CaloriesBurned { get; set; }

        public double DistanceKm { get; set; }
    }
}
