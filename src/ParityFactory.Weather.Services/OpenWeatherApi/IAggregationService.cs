using System.Threading.Tasks;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public interface IAggregationService
    {
        Task AggregateAsync();
    }
}
