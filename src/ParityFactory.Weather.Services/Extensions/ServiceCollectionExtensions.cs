using Microsoft.Extensions.DependencyInjection;
using ParityFactory.Weather.Services.OpenWeatherApi;

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

            return services;
        }
    }
}