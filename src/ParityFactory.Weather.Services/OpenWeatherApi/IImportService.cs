using System.Threading.Tasks;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public interface IImportService
    {
        Task ImportAsync();
    }
}
