using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public interface IDownloadService
    {
        Task<IEnumerable<string>> DownloadAsync(IEnumerable<string> cities);
    }
}
