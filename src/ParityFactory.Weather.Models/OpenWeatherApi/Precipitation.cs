using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Precipitation
    {
        [JsonPropertyName("1h")]
        public float? VolumeInPastHour { get; set; }

        [JsonPropertyName("3h")]
        public float? VolumeInPastThreeHours { get; set; }
    }
}
