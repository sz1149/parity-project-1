using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class AggregationServiceTests
    {
        private readonly AggregationService _aggregationService;

        public AggregationServiceTests()
        {
            _aggregationService = new AggregationService();
        }

        [TestMethod]
        public async Task Test_That_AggregationService_AggregateAsync_Throws_NotImplementedException()
        {
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _aggregationService.AggregateAsync());
        }
    }
}