using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Services.OpenWeatherApi;
using ParityFactory.Weather.Test.unit.Fakes;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class DownloadServiceTests
    {
        public DownloadServiceTests()
        {
            Environment.SetEnvironmentVariable("DATA_DIRECTORY", "./data");
            Environment.SetEnvironmentVariable("WEATHER_API_ENDPOINT",
                "http://api.openweathermap.org/data/2.5/forecast");
            Environment.SetEnvironmentVariable("WEATHER_API_ENDPOINTKEY", "9e1682bbea28fc8a3465ba65eefe75a3");
            Environment.SetEnvironmentVariable("MAX_CONCURRENCY", "4");
        }

        [TestMethod]
        public async Task Test_That_DownloadService_DownloadsFile_For_City()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "JsonFiles", "CompleteApiResponse.json");

            var httpClientFactory = new FakeHttpClientFactory
            {
                ExpectedResponseCode = HttpStatusCode.OK,
                ExpectedResponse = await File.ReadAllTextAsync(apiResponseJsonFilename)
            };

            var downloadService = new DownloadService(httpClientFactory);
            var downloadedFiles = await downloadService.DownloadAsync(new[] {"des moines"});

            Assert.AreEqual(1, downloadedFiles.Count());

            foreach (var downloadedFile in downloadedFiles) File.Delete(downloadedFile);
        }
        
        [TestMethod]
        public async Task Test_That_DownloadService_Returns_Null_If_Api_Error()
        {
            var httpClientFactory = new FakeHttpClientFactory
            {
                ExpectedResponseCode = HttpStatusCode.InternalServerError, ExpectedResponse = ""
            };

            var downloadService = new DownloadService(httpClientFactory);
            var downloadedFiles = await downloadService.DownloadAsync(new[] {"des moines"});

            Assert.AreEqual(1, downloadedFiles.Count());
            Assert.IsNull(downloadedFiles.First());
        }
        
        [TestMethod]
        public async Task Test_That_DownloadService_Returns_Null_If_Exception()
        {
            var httpClientFactory = new HttpClientFactory();
            {
                ExpectedResponseCode = HttpStatusCode.InternalServerError, ExpectedResponse = ""
            };

            var downloadService = new DownloadService(httpClientFactory);
            var downloadedFiles = await downloadService.DownloadAsync(new[] {"des moines"});

            Assert.AreEqual(1, downloadedFiles.Count());
            Assert.IsNull(downloadedFiles.First());
        }
    }
}