using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Wind
    {
        [JsonPropertyName("speed")]
        public float? Speed { get; set; }

        [JsonPropertyName("deg")]
        public short? Degrees { get; set; }
    }
}
