using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ParityFactory.Weather.Models.Mappings;

namespace ParityFactory.Weather.Models.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(LocationProfile));
            return services;
        }
    }
}
