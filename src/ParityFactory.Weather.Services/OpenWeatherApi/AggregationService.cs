using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParityFactory.Weather.Data;
using ParityFactory.Weather.Models.Data;

namespace ParityFactory.Weather.Services.OpenWeatherApi
{
    public class AggregationService : IAggregationService
    {
        private readonly IDataRepository _dataRepository;

        public AggregationService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public virtual async Task AggregateAsync()
        {
            const RegionId regionId = RegionId.Iowa;
            var lastCalculated = await GetLastCalculationTimeAsync(regionId);
            var aggregationDataPoints = await GetAggregationDataPointsAsync(regionId, lastCalculated);
            if (aggregationDataPoints == null || aggregationDataPoints.Count == 0)
                return;
            var aggregates = TransformDataPoints(regionId, aggregationDataPoints);
            _dataRepository.BulkInsert("Aggregate", aggregates);
        }

        public virtual async Task<List<AggregationDataPoints>> GetAggregationDataPointsAsync(RegionId regionId, long lastCalculated)
        {
            const string aggregateSql =
                @"select w.Temperature, w.Humidity
                from dbo.Weather w
                inner join dbo.Location l on w.LocationId = l.Id
                inner join dbo.Region r on l.RegionId = r.RegionId
                where w.Timestamp > @lastCalculated
                and r.RegionId = @regionId";

            var aggregationDataPoints = await _dataRepository.QueryAsync<AggregationDataPoints>(aggregateSql,
                new { regionId = (int)regionId, lastCalculated });
            return aggregationDataPoints.ToList();
        }

        public virtual async Task<long> GetLastCalculationTimeAsync(RegionId regionId)
        {
            var timestamps = await _dataRepository.QueryAsync<long>(
                "SELECT COALESCE(MAX(Timestamp), @utcTime) AS MaxTimeStamp FROM dbo.Aggregate WHERE RegionId = @regionId",
                new { regionId = (int)regionId, utcTime = 0});
            var timestamp = timestamps.ToList().First();
            return timestamp;
        }

        public virtual List<Aggregate> TransformDataPoints(RegionId regionId,
            List<AggregationDataPoints> aggregationDataPoints)
        {
            var temperatureReadings = new List<float>();
            var humidityReadings = new List<float>();
            foreach (var dataPoint in aggregationDataPoints)
            {
                if (dataPoint.Temperature.HasValue)
                    temperatureReadings.Add(dataPoint.Temperature.Value);
                if (dataPoint.Humidity.HasValue)
                    humidityReadings.Add(dataPoint.Humidity.Value);
            }

            var calculationTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var temperatureAggregate = BuildAggregate(regionId, AggregateTypeId.Temperature, temperatureReadings, calculationTime);
            var humidityAggregate = BuildAggregate(regionId, AggregateTypeId.Humidity, humidityReadings, calculationTime);
            return new List<Aggregate>{temperatureAggregate, humidityAggregate};
        }

        public static Aggregate BuildAggregate(RegionId regionId, AggregateTypeId aggregateTypeId, List<float> readings,
            long calculationTime)
        {
            var aggregate = new Aggregate
            {
                Id = Guid.NewGuid(),
                Timestamp = calculationTime,
                MinimumMeasurement = readings.Min(),
                AverageMeasurement = readings.Average(),
                MaximumMeasurement = readings.Max(),
                RecordCount = readings.Count,
                RegionId = regionId,
                AggregateTypeId = aggregateTypeId
            };
            return aggregate;
        }

        public struct AggregationDataPoints
        {
            internal float? Temperature { get; set; }
            internal float? Humidity { get; set; }
        }
    }
}