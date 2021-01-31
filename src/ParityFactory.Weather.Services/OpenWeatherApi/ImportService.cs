using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using ParityFactory.Weather.Data;
using ParityFactory.Weather.Models;
using ParityFactory.Weather.Models.Data;
using ParityFactory.Weather.Models.OpenWeatherApi;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public class ImportService : IImportService
    {
        private readonly IMapper _autoMapping;
        private readonly string _dataDirectory;
        private readonly IDataRepository _dataRepository;

        public ImportService(IMapper autoMapping, IDataRepository dataRepository)
        {
            _autoMapping = autoMapping;
            _dataDirectory = Environment.GetEnvironmentVariable("DATA_DIRECTORY");
            _dataRepository = dataRepository;
        }

        public virtual async Task ImportAsync()
        {
            var (apiResponses, locations) = await GetApiResponsesAsync();
            var (conditions, weatherConditions, weatherRecords) = Transform(apiResponses);
            await _dataRepository.BulkInsertAsync("RegionStaging",
                new List<Region> {new Region {Id = RegionId.Iowa, Name = "Iowa"}});
            await _dataRepository.BulkInsertAsync("LocationStaging", locations);
            await _dataRepository.BulkInsertAsync("ConditionStaging", conditions);
            await _dataRepository.BulkInsertAsync("WeatherStaging", weatherRecords);
            await _dataRepository.BulkInsertAsync("WeatherConditionStaging", weatherConditions);
            await _dataRepository.BulkInsertAsync("ConditionStaging", conditions);
        }

        public virtual async Task<(List<CurrentWeatherResponse>, List<Location>)> GetApiResponsesAsync()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var apiResponses = new List<CurrentWeatherResponse>();
            var cities = new List<Location>();
            var filesToImport = DirectoryUtil.GetAllUnprocessedFiles(_dataDirectory);
            foreach (var file in filesToImport)
            {
                var contents = await File.ReadAllTextAsync(file);
                var apiResponse = JsonSerializer.Deserialize<CurrentWeatherResponse>(contents);
                apiResponses.Add(apiResponse);
                cities.Add(_autoMapping.Map<City, Location>(apiResponse.City));
            }

            stopWatch.Stop();
            Console.WriteLine(
                $"Loaded {apiResponses.Count} unprocessed files in {stopWatch.Elapsed.TotalMilliseconds}ms");
            return (apiResponses, cities);
        }

        public (List<Condition>, List<WeatherCondition>, List<Models.Data.Weather>) Transform(
            IEnumerable<CurrentWeatherResponse> apiResponses)
        {
            var conditionDictionary = new Dictionary<short, Condition>();
            var weatherConditions = new List<WeatherCondition>();
            var weatherRecords = new List<Models.Data.Weather>();
            foreach (var apiResponse in apiResponses)
            {
                foreach (var weatherObservation in apiResponse.WeatherObservations)
                {
                    var weatherRecord = _autoMapping.Map<WeatherObservation, Models.Data.Weather>(weatherObservation);
                    weatherRecord.Sunrise = apiResponse.City.Sunrise;
                    weatherRecord.Sunset = apiResponse.City.Sunset;
                    weatherRecord.TimezoneOffset = apiResponse.City.TimezoneOffset;
                    weatherRecord.LocationId = apiResponse.City.Id;
                    weatherRecords.Add(weatherRecord);

                    foreach (var condition in weatherObservation.Conditions)
                    {
                        var weatherCondition = new WeatherCondition
                        {
                            ConditionId = condition.Id,
                            Timestamp = weatherObservation.Timestamp,
                            LocationId = apiResponse.City.Id
                        };
                        weatherConditions.Add(weatherCondition);
                        if (!conditionDictionary.ContainsKey(condition.Id))
                            conditionDictionary.Add(condition.Id, condition);
                    }
                }
            }

            return (conditionDictionary.Values.ToList(), weatherConditions, weatherRecords);
        }
    }
}