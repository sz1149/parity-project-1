using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class Precipitation
    {
        [JsonPropertyName("1h")]
        public short? VolumeInPastHour { get; set; }
        [JsonPropertyName("3h")]
        public short? VolumeInPastThreeHours { get; set; }
    }
}
