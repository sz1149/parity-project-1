using System;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Data;
using ParityFactory.Weather.Models.Extensions;
using ParityFactory.Weather.Services.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Services.OpenWeatherApi
{
    [TestClass]
    public class ImportServiceTests
    {
        private readonly ImportService _importService;

        public ImportServiceTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddAutoMapping()
                .BuildServiceProvider();

            _importService = new ImportService(serviceProvider.GetService<IMapper>(), A.Fake<IDataRepository>());
        }

        [TestMethod]
        public async Task Test_That_ImportService_ImportAsync_Throws_NotImplementedException()
        {
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _importService.ImportAsync());
        }
    }
}