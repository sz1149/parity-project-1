using System;
using Microsoft.Extensions.DependencyInjection;
using ParityFactory.Weather.Services.OpenWeatherApi;
using Polly;

namespace ParityFactory.Weather.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAggregationService, AggregationService>()
                .AddScoped<IDownloadService, DownloadService>()
                .AddScoped<IImportService, ImportService>();

            services.AddHttpClient("OpenWeatherApi")
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }))
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(10)
                ));

            return services;
        }
    }
}
