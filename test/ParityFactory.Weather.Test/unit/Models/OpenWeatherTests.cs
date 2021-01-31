using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParityFactory.Weather.Models.OpenWeatherApi;

namespace ParityFactory.Weather.Test.unit.Models
{
    [TestClass]
    public class OpenWeatherTests
    {
        [TestMethod]
        public void Test_CurrentWeatherResponse_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "JsonFiles", "CompleteApiResponseForCity.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var currentWeather = JsonSerializer.Deserialize<CurrentWeatherResponse>(apiResponseText);

            Assert.AreEqual(4853828, currentWeather.City.Id);
            Assert.AreEqual("Des Moines", currentWeather.City.Name);
            Assert.AreEqual(1612013281, currentWeather.City.Sunrise);
            Assert.AreEqual(1612049244, currentWeather.City.Sunset);
            Assert.AreEqual(-21600, currentWeather.City.TimezoneOffset);

            Assert.AreEqual(41.6005f, currentWeather.City.Coordinate.Latitude);
            Assert.AreEqual(-93.6091f, currentWeather.City.Coordinate.Longitude);

            Assert.AreEqual(40, currentWeather.WeatherReadings.Count);

            var weatherReading = currentWeather.WeatherReadings.First();
            Assert.AreEqual(1612072800, weatherReading.Timestamp);

            Assert.AreEqual(1, weatherReading.Observations.Length);
            Assert.AreEqual(601, weatherReading.Observations[0].Id);
            Assert.AreEqual("Snow", weatherReading.Observations[0].Title);
            Assert.AreEqual("snow", weatherReading.Observations[0].Description);
            Assert.AreEqual("13n", weatherReading.Observations[0].Icon);

            Assert.AreEqual(272.86f, weatherReading.Measurement.Temperature);
            Assert.AreEqual(266.42f, weatherReading.Measurement.FeelsLikeTemperature);
            Assert.AreEqual(272.86f, weatherReading.Measurement.MinimumTemperature);
            Assert.AreEqual(272.94f, weatherReading.Measurement.MaximumTemperature);
            Assert.AreEqual((short)1008, weatherReading.Measurement.Pressure);
            Assert.AreEqual((byte)96, weatherReading.Measurement.Humidity);

            Assert.AreEqual((byte)95, weatherReading.Cloud.PercentCloudiness);

            Assert.AreEqual(6.19f, weatherReading.Wind.Speed);
            Assert.AreEqual((short)356, weatherReading.Wind.Degrees);

            Assert.AreEqual(1.71f, weatherReading.Snow.VolumeInPastThreeHours);
        }

        [TestMethod]
        public void Test_Weather_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "JsonFiles", "CompleteWeatherRecord.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Weather.Models.OpenWeatherApi.Weather>(apiResponseText);

            Assert.AreEqual(1601055181, apiResponse.Timestamp);

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
        }

        /// <summary>
        /// Test for missing models: "Only really measured or calculated data is displayed in API response."
        /// Reference: https://openweathermap.org/current#list
        /// </summary>
        [TestMethod]
        public void Test_Weather_With_MissingModels_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "JsonFiles", "MissingModelsResponse.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Weather.Models.OpenWeatherApi.Weather>(apiResponseText);

            Assert.AreEqual(1601055181, apiResponse.Timestamp);

            Assert.IsNull(apiResponse.Observations);
            Assert.IsNull(apiResponse.Measurement);
            Assert.IsNull(apiResponse.Cloud);
            Assert.IsNull(apiResponse.Rain);
            Assert.IsNull(apiResponse.Snow);
            Assert.IsNull(apiResponse.Wind);
        }

        [TestMethod]
        public void Test_Weather_With_MissingModelProperties_Deserializes_To_Object()
        {
            var apiResponseJsonFilename =
                Path.Combine("unit", "JsonFiles", "MissingModelPropertiesResponse.json");
            var apiResponseText = File.ReadAllText(apiResponseJsonFilename);
            var apiResponse = JsonSerializer.Deserialize<Weather.Models.OpenWeatherApi.Weather>(apiResponseText);

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
        }
    }
}
