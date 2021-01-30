using System.IO;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Models.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Models
{
    [TestClass]
    public class OpenWeatherTests
    {
        [TestMethod]
        public void Test_ApiResponse_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "Models", "JsonFiles", "CompleteApiResponse.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Response>(apiResponseText);

            Assert.AreEqual("stations", apiResponse.Base);
            Assert.AreEqual(1601055181, apiResponse.Timestamp);
            Assert.AreEqual(3600, apiResponse.TimezoneOffset);
            Assert.AreEqual(2643743, apiResponse.Id);
            Assert.AreEqual("London", apiResponse.Name);
            Assert.AreEqual(200, apiResponse.Cod);

            Assert.AreEqual(51.51f, apiResponse.Coordinate.Latitude);
            Assert.AreEqual(-0.13f, apiResponse.Coordinate.Longitude);

            Assert.AreEqual(1, apiResponse.Observations.Length);
            Assert.AreEqual(803, apiResponse.Observations[0].Id);
            Assert.AreEqual("Clouds", apiResponse.Observations[0].Title);
            Assert.AreEqual("broken clouds", apiResponse.Observations[0].Description);
            Assert.AreEqual("04d", apiResponse.Observations[0].Icon);

            Assert.AreEqual(286.33f, apiResponse.Measurement.Temperature);
            Assert.AreEqual(278.88f, apiResponse.Measurement.FeelsLikeTemperature);
            Assert.AreEqual(285.37f, apiResponse.Measurement.MinimumTemperature);
            Assert.AreEqual(287.04f, apiResponse.Measurement.MaximumTemperature);
            Assert.AreEqual((short)1007, apiResponse.Measurement.Pressure);
            Assert.AreEqual((byte)42, apiResponse.Measurement.Humidity);

            Assert.AreEqual((byte)77, apiResponse.Cloud.PercentCloudiness);

            Assert.AreEqual(7.93f, apiResponse.Wind.Speed);
            Assert.AreEqual((short)314, apiResponse.Wind.Degrees);

            Assert.AreEqual((short)200, apiResponse.Rain.VolumeInPastHour);
            Assert.AreEqual((short)800, apiResponse.Rain.VolumeInPastThreeHours);

            Assert.AreEqual((short)200, apiResponse.Snow.VolumeInPastHour);
            Assert.AreEqual((short)800, apiResponse.Snow.VolumeInPastThreeHours);

            Assert.AreEqual("GB", apiResponse.SystemData.CountryCode);
            Assert.AreEqual(1601013115, apiResponse.SystemData.Sunrise);
            Assert.AreEqual(1601056333, apiResponse.SystemData.Sunset);
        }

        /// <summary>
        /// Test for missing models: "Only really measured or calculated data is displayed in API response."
        /// Reference: https://openweathermap.org/current#list
        /// </summary>
        [TestMethod]
        public void Test_ApiResponse_With_MissingModels_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "Models", "JsonFiles", "MissingModelsResponse.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Response>(apiResponseText);

            Assert.AreEqual("stations", apiResponse.Base);
            Assert.AreEqual(1601055181, apiResponse.Timestamp);
            Assert.AreEqual(3600, apiResponse.TimezoneOffset);
            Assert.AreEqual(2643743, apiResponse.Id);
            Assert.AreEqual("London", apiResponse.Name);
            Assert.AreEqual(200, apiResponse.Cod);

            Assert.AreEqual(51.51f, apiResponse.Coordinate.Latitude);
            Assert.AreEqual(-0.13f, apiResponse.Coordinate.Longitude);

            Assert.IsNull(apiResponse.Observations);
            Assert.IsNull(apiResponse.Measurement);
            Assert.IsNull(apiResponse.Cloud);
            Assert.IsNull(apiResponse.Rain);
            Assert.IsNull(apiResponse.Snow);
            Assert.IsNull(apiResponse.Wind);

            Assert.AreEqual("GB", apiResponse.SystemData.CountryCode);
            Assert.AreEqual(1601013115, apiResponse.SystemData.Sunrise);
            Assert.AreEqual(1601056333, apiResponse.SystemData.Sunset);
        }

        [TestMethod]
        public void Test_ApiResponse_With_MissingModelProperties_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "Models", "JsonFiles", "MissingModelPropertiesResponse.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Response>(apiResponseText);

            Assert.AreEqual("stations", apiResponse.Base);
            Assert.AreEqual(1601055181, apiResponse.Timestamp);
            Assert.AreEqual(3600, apiResponse.TimezoneOffset);
            Assert.AreEqual(2643743, apiResponse.Id);
            Assert.AreEqual("London", apiResponse.Name);
            Assert.AreEqual(200, apiResponse.Cod);

            Assert.AreEqual(51.51f, apiResponse.Coordinate.Latitude);
            Assert.AreEqual(-0.13f, apiResponse.Coordinate.Longitude);

            Assert.AreEqual(0, apiResponse.Observations.Length);

            Assert.IsNull(apiResponse.Measurement.Temperature);
            Assert.IsNull(apiResponse.Measurement.FeelsLikeTemperature);
            Assert.IsNull(apiResponse.Measurement.MinimumTemperature);
            Assert.IsNull(apiResponse.Measurement.MaximumTemperature);
            Assert.IsNull(apiResponse.Measurement.Pressure);
            Assert.IsNull(apiResponse.Measurement.Humidity);

            Assert.IsNull(apiResponse.Cloud.PercentCloudiness);

            Assert.IsNull(apiResponse.Rain.VolumeInPastHour);
            Assert.IsNull(apiResponse.Rain.VolumeInPastThreeHours);

            Assert.IsNull(apiResponse.Snow.VolumeInPastHour);
            Assert.IsNull(apiResponse.Snow.VolumeInPastThreeHours);

            Assert.IsNull(apiResponse.Wind.Speed);
            Assert.IsNull(apiResponse.Wind.Degrees);

            Assert.AreEqual("GB", apiResponse.SystemData.CountryCode);
            Assert.AreEqual(1601013115, apiResponse.SystemData.Sunrise);
            Assert.AreEqual(1601056333, apiResponse.SystemData.Sunset);
        }
    }
}
