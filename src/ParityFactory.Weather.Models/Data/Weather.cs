using System;

namespace ParityFactory.Weather.Models.Data
{
    public class Weather
    {
        public Guid Id { get; set; }
        public long LocationId { get; set; }
        public long Timestamp { get; set; }
        public short? PercentCloudiness { get; set; }
        public float? Temperature { get; set; }
        public float? FeelsLikeTemperature { get; set; }
        public float? MinimumTemperature { get; set; }
        public float? MaximumTemperature { get; set; }
        public short? Pressure { get; set; }
        public short? Humidity { get; set; }
        public float? RainInPastHour { get; set; }
        public float? RainInPastThreeHours { get; set; }
        public float? SnowInPastHour { get; set; }
        public float? SnowInPastThreeHours { get; set; }
        public float? WindSpeed { get; set; }
        public short? WindDirectionDegrees { get; set; }
        public int? Visibility { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public int TimezoneOffset { get; set; }
    }
}
