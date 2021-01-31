using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParityFactory.Weather.Test.unit
{
    [TestClass]
    public class ProgramTests
    {
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
    }
}
