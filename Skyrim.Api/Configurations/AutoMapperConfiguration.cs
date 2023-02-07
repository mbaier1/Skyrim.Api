using AutoMapper;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CreateLocationDto>().ReverseMap();
            CreateMap<Town, CreateLocationDto>().ReverseMap();
            CreateMap<Homestead, CreateLocationDto>().ReverseMap();
            CreateMap<Settlement, CreateLocationDto>().ReverseMap();
            CreateMap<DaedricShrine, CreateLocationDto>().ReverseMap();
            CreateMap<StandingStone, CreateLocationDto>().ReverseMap();
        }
    }
}
