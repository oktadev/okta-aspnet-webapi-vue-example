using System;

namespace WeightTrackerOkta.Models
{
    public class WeightMeasurement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}