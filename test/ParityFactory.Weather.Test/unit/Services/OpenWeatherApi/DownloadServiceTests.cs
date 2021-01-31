using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class DownloadServiceTests
    {
        private readonly DownloadService _downloadService;

        public DownloadServiceTests()
        {
            _downloadService = new DownloadService();
        }

        [TestMethod]
        public async Task Test_That_DownloadService_DownloadAsync_Throws_NotImplementedException()
        {
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _downloadService.DownloadAsync());
        }
    }
}