using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class SystemData
    {
        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("sunrise")]
        public long? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long? Sunset { get; set; }
    }
}
