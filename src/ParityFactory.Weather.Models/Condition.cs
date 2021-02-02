using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ParityFactory.Weather.Models
{
    [Keyless]
    public class Condition
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
