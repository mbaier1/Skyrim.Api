using AutoMapper;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, LocationDto>().ReverseMap();
            CreateMap<Town, LocationDto>().ReverseMap();
            CreateMap<Homestead, LocationDto>().ReverseMap();
            CreateMap<Settlement, LocationDto>().ReverseMap();
            CreateMap<DaedricShrine, LocationDto>().ReverseMap();
            CreateMap<StandingStone, LocationDto>().ReverseMap();
            CreateMap<Landmark, LocationDto>().ReverseMap();
            CreateMap<Camp, LocationDto>().ReverseMap();
            CreateMap<Cave, LocationDto>().ReverseMap();
            CreateMap<Clearing, LocationDto>().ReverseMap();
            CreateMap<Dock, LocationDto>().ReverseMap();
            CreateMap<DragonLair, LocationDto>().ReverseMap();
            CreateMap<DwarvenRuin, LocationDto>().ReverseMap();
            CreateMap<Farm, LocationDto>().ReverseMap();
            CreateMap<Fort, LocationDto>().ReverseMap();
            CreateMap<GiantCamp, LocationDto>().ReverseMap();
            CreateMap<Grove, LocationDto>().ReverseMap();
            CreateMap<ImperialCamp, LocationDto>().ReverseMap();
            CreateMap<LightHouse, LocationDto>().ReverseMap();
            CreateMap<Mine, LocationDto>().ReverseMap();
            CreateMap<NordicTower, LocationDto>().ReverseMap();
            CreateMap<OrcStronghold, LocationDto>().ReverseMap();
            CreateMap<Pass, LocationDto>().ReverseMap();
            CreateMap<Ruin, LocationDto>().ReverseMap();
            CreateMap<Shack, LocationDto>().ReverseMap();
            CreateMap<Ship, LocationDto>().ReverseMap();
            CreateMap<Shipwreck, LocationDto>().ReverseMap();
            CreateMap<Stable, LocationDto>().ReverseMap();
            CreateMap<StormcloakCamp, LocationDto>().ReverseMap();
            CreateMap<Tomb, LocationDto>().ReverseMap();
            CreateMap<Watchtower, LocationDto>().ReverseMap();
            CreateMap<WheatMill, LocationDto>().ReverseMap();
            CreateMap<LumberMill, LocationDto>().ReverseMap();
            CreateMap<BodyOfWater, LocationDto>().ReverseMap();
            CreateMap<InnOrTavern, LocationDto>().ReverseMap();
            CreateMap<Temple, LocationDto>().ReverseMap();
            CreateMap<WordWall, LocationDto>().ReverseMap();
            CreateMap<Castle, LocationDto>().ReverseMap();
            CreateMap<GuildHeadquarter, LocationDto>().ReverseMap();
            CreateMap<UnmarkedLocation, LocationDto>().ReverseMap();
        }
    }
}
