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
            var (filesToImport, apiResponses, locations) = await GetApiResponsesAsync();
            var (conditions, weatherConditions, weatherRecords) = Transform(apiResponses);

            _dataRepository.BulkInsert("staging.Location", locations);
            await _dataRepository.ExecuteStoredProcedureAsync("dbo.ImportLocations", null);
            // todo: migrate proc
            _dataRepository.BulkInsert("staging.Condition", conditions);
            await _dataRepository.ExecuteStoredProcedureAsync("dbo.ImportConditions", null);
            // todo: migrate proc
            _dataRepository.BulkInsert("dbo.Weather", weatherRecords);
            _dataRepository.BulkInsert("dbo.WeatherCondition", weatherConditions);
            
            DirectoryUtil.ArchiveFiles(_dataDirectory, filesToImport, DateTime.UtcNow);
        }

        public virtual async Task<(string[], List<CurrentWeatherResponse>, List<Location>)> GetApiResponsesAsync()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var apiResponses = new List<CurrentWeatherResponse>();
            var locations = new Dictionary<long, Location>();
            var filesToImport = DirectoryUtil.GetAllUnprocessedFiles(_dataDirectory);
            foreach (var file in filesToImport)
            {
                var contents = await File.ReadAllTextAsync(file);
                var apiResponse = JsonSerializer.Deserialize<CurrentWeatherResponse>(contents);
                apiResponses.Add(apiResponse);
                var location = _autoMapping.Map<City, Location>(apiResponse.City);
                if (!locations.ContainsKey(location.Id))
                    locations.Add(location.Id, location);
            }

            stopWatch.Stop();
            Console.WriteLine(
                $"Loaded {apiResponses.Count} unprocessed files in {stopWatch.Elapsed.TotalMilliseconds}ms");
            return (filesToImport, apiResponses, locations.Values.ToList());
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
                            WeatherId = weatherRecord.Id
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