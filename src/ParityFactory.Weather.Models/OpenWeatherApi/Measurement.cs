using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Measurement
    {
        [JsonPropertyName("temp")]
        public float? Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public float? FeelsLikeTemperature { get; set; }

        [JsonPropertyName("temp_min")]
        public float? MinimumTemperature { get; set; }

        [JsonPropertyName("temp_max")]
        public float? MaximumTemperature { get; set; }

        [JsonPropertyName("pressure")]
        public short? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public byte? Humidity { get; set; }
    }
}
