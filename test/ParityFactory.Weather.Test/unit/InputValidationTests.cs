using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParityFactory.Weather.Test.unit
{
    [TestClass]
    public class InputValidationTests
    {
        [TestMethod]
        public void Test_InputValidation_Returns_False_If_Empty_ArgumentList()
        {
            Assert.IsFalse(InputValidation.IsValid(System.Array.Empty<string>()));
        }

        [TestMethod]
        public void Test_InputValidation_Returns_False_For_Invalid_Argument()
        {
            Assert.IsFalse(InputValidation.IsValid(new [] {"not a valid argument"}));
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("aggregate")]
        [DataRow("download")]
        [DataRow("import")]
        public void Test_InputValidation_Returns_True_For_Valid_Argument(string argument)
        {
            Assert.IsTrue(InputValidation.IsValid(new [] {argument}));
        }
    }
}
