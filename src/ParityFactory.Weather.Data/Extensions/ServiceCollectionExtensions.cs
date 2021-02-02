using Microsoft.Extensions.DependencyInjection;

namespace ParityFactory.Weather.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataRepository(this IServiceCollection services)
        {
            services.AddSingleton<IDataRepository, DataRepository>();

            return services;
        }
    }
}
