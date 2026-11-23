using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{

    [Table("HealthMeasurement")]
    public class HealthMeasurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public int MeasurementTypeId { get; set; }
        public double Value { get; set; }

        [Indexed]
        public DateTime RecordedAt { get; set; }
        public DataSource Source { get; set; } = DataSource.Manual;
    }
}
