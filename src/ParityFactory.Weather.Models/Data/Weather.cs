namespace ParityFactory.Weather.Models.Data
{
    public class Weather
    {
        public long LocationId { get; set; }
        public long Timestamp { get; set; }
        public byte? PercentCloudiness { get; set; }
        public float? Temperature { get; set; }
        public float? FeelsLikeTemperature { get; set; }
        public float? MinimumTemperature { get; set; }
        public float? MaximumTemperature { get; set; }
        public short? Pressure { get; set; }
        public byte? Humidity { get; set; }
        public float? RainInPastHour { get; set; }
        public float? RainInPastThreeHours { get; set; }
        public float? SnowInPastHour { get; set; }
        public float? SnowInPastThreeHours { get; set; }
        public float? Speed { get; set; }
        public short? Degrees { get; set; }
        public int? Visibility { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public int TimezoneOffset { get; set; }
    }
}
