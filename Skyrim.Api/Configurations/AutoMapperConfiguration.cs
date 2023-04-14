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
            CreateMap<Landmark, CreateLocationDto>().ReverseMap();
            CreateMap<Camp, CreateLocationDto>().ReverseMap();
            CreateMap<Cave, CreateLocationDto>().ReverseMap();
            CreateMap<Clearing, CreateLocationDto>().ReverseMap();
            CreateMap<Dock, CreateLocationDto>().ReverseMap();
            CreateMap<DragonLair, CreateLocationDto>().ReverseMap();
            CreateMap<DwarvenRuin, CreateLocationDto>().ReverseMap();
            CreateMap<Farm, CreateLocationDto>().ReverseMap();
            CreateMap<Fort, CreateLocationDto>().ReverseMap();
            CreateMap<GiantCamp, CreateLocationDto>().ReverseMap();
            CreateMap<Grove, CreateLocationDto>().ReverseMap();
            CreateMap<ImperialCamp, CreateLocationDto>().ReverseMap();
            CreateMap<LightHouse, CreateLocationDto>().ReverseMap();
            CreateMap<Mine, CreateLocationDto>().ReverseMap();
            CreateMap<NordicTower, CreateLocationDto>().ReverseMap();
            CreateMap<OrcStronghold, CreateLocationDto>().ReverseMap();
            CreateMap<Pass, CreateLocationDto>().ReverseMap();
            CreateMap<Ruin, CreateLocationDto>().ReverseMap();
            CreateMap<Shack, CreateLocationDto>().ReverseMap();
            CreateMap<Ship, CreateLocationDto>().ReverseMap();
            CreateMap<Shipwreck, CreateLocationDto>().ReverseMap();
            CreateMap<Stable, CreateLocationDto>().ReverseMap();
            CreateMap<StormcloakCamp, CreateLocationDto>().ReverseMap();
        }
    }
}
