using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit
{
    [TestClass]
    public class CommandHandlerTests
    {
        private readonly IAggregationService _aggregationService;
        private readonly IDownloadService _downloadService;
        private readonly IImportService _importService;

        private readonly CommandHandler _commandHandler;

        public CommandHandlerTests()
        {
            _aggregationService = A.Fake<IAggregationService>();
            _downloadService = A.Fake<IDownloadService>();
            _importService = A.Fake<IImportService>();
            _commandHandler = new CommandHandler(_aggregationService, _downloadService, _importService);
        }

        [TestMethod]
        public void Test_ExecuteAsync_With_AggregateCommand_Calls_AggregationService_AggregateAsync()
        {
            _commandHandler.ExecuteAsync("aggregate");

            A.CallTo(() => _aggregationService.AggregateAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _downloadService.DownloadAsync(A<string[]>._)).MustNotHaveHappened();
            A.CallTo(() => _importService.ImportAsync()).MustNotHaveHappened();
        }

        [TestMethod]
        public void Test_ExecuteAsync_With_DownloadCommand_Calls_DownloadService_AggregateAsync()
        {
            _commandHandler.ExecuteAsync("download");

            A.CallTo(() => _aggregationService.AggregateAsync()).MustNotHaveHappened();
            A.CallTo(() => _downloadService.DownloadAsync(
                A<string[]>.That.Matches(cities => cities.Length == 10)))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _importService.ImportAsync()).MustNotHaveHappened();
        }

        [TestMethod]
        public void Test_ExecuteAsync_With_ImportCommand_Calls_ImportService_ImportAsync()
        {
            _commandHandler.ExecuteAsync("import");

            A.CallTo(() => _aggregationService.AggregateAsync()).MustNotHaveHappened();
            A.CallTo(() => _downloadService.DownloadAsync(A<string[]>._)).MustNotHaveHappened();
            A.CallTo(() => _importService.ImportAsync()).MustHaveHappenedOnceExactly();
        }
    }
}