using System;

namespace ParityFactory.Weather.Models.Data
{
    public class Aggregate
    {
        public Guid Id { get; set; }
        public RegionId RegionId { get; set; }
        public AggregateTypeId AggregateTypeId { get; set; }
        public float MinimumMeasurement { get; set; }
        public float AverageMeasurement { get; set; }
        public float MaximumMeasurement { get; set; }
        public int RecordCount { get; set; }
        public long Timestamp { get; set; }
    }
}
