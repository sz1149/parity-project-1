using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Observation
    {
        [JsonPropertyName("id")]
        public short Id { get; set; }

        [JsonPropertyName("main")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}
