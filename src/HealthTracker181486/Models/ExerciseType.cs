using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    [Table("ExerciseType")]
    public class ExerciseType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; } = string.Empty;
        public string IconValue { get; set; } = string.Empty;
        public double MetValue { get; set; }
        public bool TracksDistance { get; set; }
    }
}
