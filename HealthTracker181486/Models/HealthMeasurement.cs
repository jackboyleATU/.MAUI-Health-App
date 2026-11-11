using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{

    public enum MeasurementType
    {
        HeartRate = 0,
        BloodPressure = 1
    }

    [Table("HealthMeasurement")]
    public class HealthMeasurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public MeasurementType Type { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;

        [Indexed]
        public DateTime RecordedAt { get; set; }
        public DataSource Source { get; set; } = DataSource.Manual;
    }
}
