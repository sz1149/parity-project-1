using AutoMapper;
using ParityFactory.Weather.Models.Data;
using ParityFactory.Weather.Models.OpenWeatherApi;

namespace ParityFactory.Weather.Models.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<City, Location>()
                .ForMember(dest =>
                    dest.RegionId, opt => opt.MapFrom(src => RegionId.Iowa))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coordinate.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coordinate.Longitude));
        }
    }
}