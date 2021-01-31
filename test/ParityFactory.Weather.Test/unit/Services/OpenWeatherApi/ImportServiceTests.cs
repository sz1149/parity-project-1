using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class ImportServiceTests
    {
        private readonly ImportService _importService;

        public ImportServiceTests()
        {
            _importService = new ImportService();
        }

        [TestMethod]
        public async Task Test_That_ImportService_ImportAsync_Throws_NotImplementedException()
        {
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _importService.ImportAsync());
        }
    }
}