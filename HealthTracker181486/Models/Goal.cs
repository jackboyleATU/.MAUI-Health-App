using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{

    [Table("Goal")]
    public class Goal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public int GoalTypeId { get; set; }

        public int TargetValue { get; set; }
        public bool ReminderEnabled { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
