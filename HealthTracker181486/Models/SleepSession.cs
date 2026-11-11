using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker181486.Models
{
    

    [Table("SleepSession")]
    public class SleepSession
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }  
        public int DurationMinutes { get; set; }
        public DataSource Source { get; set; } = DataSource.Manual;
    }
}
