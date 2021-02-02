using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class WeatherObservation
    {
        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("main")]
        public Measurement Measurement { get; set; }

        [JsonPropertyName("weather")]
        public Condition[] Conditions { get; set; }

        [JsonPropertyName("clouds")]
        public Cloud Cloud { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }

        [JsonPropertyName("rain")]
        public Precipitation Rain { get; set; }

        [JsonPropertyName("snow")]
        public Precipitation Snow { get; set; }
    }
}