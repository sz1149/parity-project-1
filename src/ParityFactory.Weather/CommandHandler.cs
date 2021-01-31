using System.Threading.Tasks;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IAggregationService _aggregationService;
        private readonly IDownloadService _downloadService;
        private readonly IImportService _importService;

        public CommandHandler(IAggregationService aggregationService, IDownloadService downloadService,
            IImportService importService)
        {
            _aggregationService = aggregationService;
            _downloadService = downloadService;
            _importService = importService;
        }

        public virtual async Task ExecuteAsync(string command)
        {
            if (command == "aggregate")
            {
                await _aggregationService.AggregateAsync();
            }
            if (command == "download")
            {
                await _downloadService.DownloadAsync();
            }
            if (command == "import")
            {
                await _importService.ImportAsync();
            }
        }
    }
}
