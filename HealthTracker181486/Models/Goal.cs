using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    public enum GoalType
    {
        Steps = 0,
        Calories = 1,
        Sleep = 2
    }

    [Table("Goal")]
    public class Goal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        public GoalType GoalType { get; set; }
        public int TargetValue { get; set; }
        public bool ReminderEnabled { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
