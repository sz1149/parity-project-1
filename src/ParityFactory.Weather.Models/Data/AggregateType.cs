namespace ParityFactory.Weather.Models.Data
{
    public enum AggregateTypeId
    {
        Temperature = 1,
        FeelsLikeTemperature = 2,
        PercentCloudiness = 3,
        Pressure = 4,
        Humidity = 5,
        Rain = 6,
        Snow = 7,
        WindSpeed = 8,
        Visibility = 9
    }

    public class AggregateType
    {
        public AggregateTypeId Id { get; set; }
        public string Name { get; set; }
    }
}
