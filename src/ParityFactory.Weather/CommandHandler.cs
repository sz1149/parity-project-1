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
            switch (command)
            {
                case "aggregate":
                    await _aggregationService.AggregateAsync();
                    break;
                case "download":
                    // TODO: config variable?  user input?
                    var cities = new[]
                    {
                        "des moines", "johnston", "norwalk", "cedar rapids", "council bluffs", "davenport",
                        "fort dodge", "mason city", "sioux city", "altoona"
                    };
                    await _downloadService.DownloadAsync(cities);
                    break;
                case "import":
                    await _importService.ImportAsync();
                    break;
            }
        }
    }
}
