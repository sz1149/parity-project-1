using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Cloud
    {
        [JsonPropertyName("all")]
        public byte? PercentCloudiness { get; set; }
    }
}
