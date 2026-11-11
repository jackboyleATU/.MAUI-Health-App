using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HealthTracker181486.Models
{
    public enum Sex
    {
        NotSpecified = 0,
        Male = 1,
        Female = 2,

        PreferNotToSay = 3
    }

    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; } = Sex.NotSpecified;
        public double HeightCm { get; set; }
        public double WeightKg { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
