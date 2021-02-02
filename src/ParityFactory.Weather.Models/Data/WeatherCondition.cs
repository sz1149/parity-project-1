using System;

namespace ParityFactory.Weather.Models.Data
{
    public class WeatherCondition
    {
        public Guid WeatherId { get; set; }
        public short ConditionId { get; set; }
    }
}
