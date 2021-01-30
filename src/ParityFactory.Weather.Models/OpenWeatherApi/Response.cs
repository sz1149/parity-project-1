using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Response
    {
        [JsonPropertyName("coord")]
        public Coordinate Coordinate { get; set; }

        [JsonPropertyName("weather")]
        public Observation[] Observations { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("main")]
        public Measurement Measurement { get; set; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("clouds")]
        public Cloud Cloud { get; set; }

        [JsonPropertyName("rain")]
        public Precipitation Rain { get; set; }

        [JsonPropertyName("snow")]
        public Precipitation Snow { get; set; }

        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }

        [JsonPropertyName("timezone")]
        public int TimezoneOffset { get; set; }

        [JsonPropertyName("sys")]
        public SystemData SystemData { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]        
        public string Name { get; set; }

        [JsonPropertyName("cod")]
        public int? Cod { get; set; }
    }
}
