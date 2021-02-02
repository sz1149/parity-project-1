using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParityFactory.Weather.Test.unit
{
    [TestClass]
    public class ProgramTests
    {
        public ProgramTests()
        {
            TestHelpers.ConfigureEnvironment();
        }

        [TestMethod]
        public void Test_That_Program_Builds_ServiceProvider_For_All_InterfacesInProject()
        {
            var serviceProvider = Program.BuildServiceProvider();
            var interfacesInServices = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Where(a => a.Name.StartsWith("ParityFactory.Weather"))
                .SelectMany(a => Assembly.Load(a).GetTypes().Where(t => t.IsInterface));

            interfacesInServices.ToList().ForEach(interfaceInServices =>
            {
                var providedService = serviceProvider.GetService(interfaceInServices);
                Assert.IsNotNull(providedService);
                Assert.IsInstanceOfType(providedService, interfaceInServices);
            });
        }

        [TestMethod]
        public void Test_That_Program_Builds_Configures_AutoMapper()
        {
            var serviceProvider = Program.BuildServiceProvider();
            var mapperConfig = serviceProvider.GetRequiredService<IConfigurationProvider>();
            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
