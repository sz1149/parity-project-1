using System;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Data;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class AggregationServiceTests
    {
        private readonly AggregationService _aggregationService;
        private readonly IDataRepository _dataRepository;

        public AggregationServiceTests()
        {
            _dataRepository = A.Fake<IDataRepository>();
            _aggregationService = new AggregationService(_dataRepository);
        }
    }
}