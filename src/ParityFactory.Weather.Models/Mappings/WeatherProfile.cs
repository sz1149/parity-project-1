using System;
using AutoMapper;
using ParityFactory.Weather.Models.OpenWeatherApi;

namespace ParityFactory.Weather.Models.Mappings
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherObservation, Data.Weather>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.LocationId, opt => opt.Ignore()) // set post mapping
                .ForMember(dest => dest.PercentCloudiness,
                    opt => opt.MapFrom(src => src.Cloud != null ? src.Cloud.PercentCloudiness : null))
                .ForMember(dest => dest.Temperature,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.Temperature : null))
                .ForMember(dest => dest.FeelsLikeTemperature,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.FeelsLikeTemperature : null))
                .ForMember(dest => dest.MinimumTemperature,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.MinimumTemperature : null))
                .ForMember(dest => dest.MaximumTemperature,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.MaximumTemperature : null))
                .ForMember(dest => dest.Pressure,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.Pressure : null))
                .ForMember(dest => dest.Humidity,
                    opt => opt.MapFrom(src => src.Measurement != null ? src.Measurement.Humidity : null))
                .ForMember(dest => dest.RainInPastHour,
                    opt => opt.MapFrom(src => src.Rain != null ? src.Rain.VolumeInPastHour : null))
                .ForMember(dest => dest.RainInPastThreeHours,
                    opt => opt.MapFrom(src => src.Rain != null ? src.Rain.VolumeInPastThreeHours : null))
                .ForMember(dest => dest.SnowInPastHour,
                    opt => opt.MapFrom(src => src.Snow != null ? src.Snow.VolumeInPastHour : null))
                .ForMember(dest => dest.SnowInPastThreeHours,
                    opt => opt.MapFrom(src => src.Snow != null ? src.Snow.VolumeInPastThreeHours : null))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind != null ? src.Wind.Speed : null))
                .ForMember(dest => dest.WindDirectionDegrees, opt => opt.MapFrom(src => src.Wind != null ? src.Wind.Degrees : null))
                .ForMember(dest => dest.TimezoneOffset, opt => opt.Ignore()) // set post mapping
                .ForMember(dest => dest.Sunrise, opt => opt.Ignore()) // set post mapping
                .ForMember(dest => dest.Sunset, opt => opt.Ignore()) // set post mapping
                ;
        }
    }
}