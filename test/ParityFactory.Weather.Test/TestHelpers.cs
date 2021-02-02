using System;

namespace ParityFactory.Weather.Test
{
    public static class TestHelpers
    {
        public static void ConfigureEnvironment()
        {
            Environment.SetEnvironmentVariable("DATA_DIRECTORY", "./data");
            Environment.SetEnvironmentVariable("WEATHER_API_ENDPOINT",
                "http://api.openweathermap.org/data/2.5/forecast");
            Environment.SetEnvironmentVariable("WEATHER_API_ENDPOINTKEY", "");
            Environment.SetEnvironmentVariable("MAX_CONCURRENCY", "4");
        }
    }
}