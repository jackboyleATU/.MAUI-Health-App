using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker181486.Models
{
    [Table("MeasurementType")]
    public class MeasurementType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
}
