using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ParityFactory.Weather.Models.OpenWeatherApi
{
    public class CurrentWeatherResponse
    {
        [JsonPropertyName("list")]
        public List<WeatherObservation> WeatherObservations { get; set; }

        [JsonPropertyName("city")]
        public City City { get; set; }
    }
}
