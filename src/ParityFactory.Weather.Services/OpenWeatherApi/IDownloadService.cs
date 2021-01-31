using System.Threading.Tasks;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public interface IDownloadService
    {
        Task DownloadAsync();
    }
}
