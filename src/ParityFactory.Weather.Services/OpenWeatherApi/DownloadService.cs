using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public class DownloadService : IDownloadService
    {
        private readonly string _country;
        private readonly string _dataDirectory;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly string _endpointAuthorization;
        private readonly int _maxConcurrency;
        private readonly SemaphoreSlim _semaphore;

        private bool isEndpointAvailable = true;

        public DownloadService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OpenWeatherApi");
            _dataDirectory = Environment.GetEnvironmentVariable("DATA_DIRECTORY");
            _endpoint = Environment.GetEnvironmentVariable("WEATHER_API_ENDPOINT");
            _endpointAuthorization = Environment.GetEnvironmentVariable("WEATHER_API_ENDPOINT_KEY");
            _maxConcurrency = Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_CONCURRENCY"));
            _semaphore = new SemaphoreSlim(_maxConcurrency);

            ServicePointManager.FindServicePoint(new Uri(_endpoint)).ConnectionLimit = _maxConcurrency;
        }

        public virtual async Task<IEnumerable<string>> DownloadAsync(IEnumerable<string> cities)
        {
            var tasks = cities.Select(GetCurrentWeatherAsync).ToList();
            await Task.WhenAll(tasks);
            var downloadedFiles = tasks.Select(task => task.Result).ToList();
            return downloadedFiles;
        }

        public virtual async Task<string> GetCurrentWeatherAsync(string city)
        {
            await _semaphore.WaitAsync();

            string downloadedFilename = null;
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                
                var url = GetCurrentWeatherUrl(city);
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    downloadedFilename = await SaveAsync(city, await response.Content.ReadAsStreamAsync());
                    stopWatch.Stop();
                    Console.WriteLine($"Saved data for {city} to {downloadedFilename} in {stopWatch.Elapsed.TotalMilliseconds}ms");    
                }
                else
                {
                    Console.WriteLine($"Unable to retrieve data for {city}"); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _semaphore.Release();
            }

            return downloadedFilename;
        }

        public virtual string GetCurrentWeatherUrl(string city)
        {
            return $"{_endpoint}?q={city},ia,us&APPID={_endpointAuthorization}";
        }

        public virtual async Task<string> SaveAsync(string city, Stream stream)
        {
            var dateTime = DateTime.UtcNow;

            var directoryToSaveTo = Path.Combine(_dataDirectory, dateTime.Year.ToString(), dateTime.Month.ToString(),
                dateTime.Day.ToString(), dateTime.Hour.ToString());
            Directory.CreateDirectory(directoryToSaveTo);

            var filenameToSave = Path.Combine(directoryToSaveTo, $"{city}_{dateTime.Minute}_{dateTime.Second}.json");
            await using var fileStream = File.Create(filenameToSave);
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(fileStream);
            await stream.DisposeAsync();
            return filenameToSave;
        }
    }
}
