using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    [Table("GoalTypes")]
    public class GoalType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int MinTarget { get; set; }
        public int MaxTarget { get; set; }
        public int DefaultTarget { get; set; }
    }
}
