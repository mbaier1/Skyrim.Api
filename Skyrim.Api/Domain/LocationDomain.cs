using AutoMapper;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;

namespace Skyrim.Api.Domain
{
    public class LocationDomain : ILocationDomain
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationDtoFormatHelper _CreateLocationDtoFormatHelper;
        private readonly IDomainLoggerExtension _loggerExtension;
        private readonly IMapper _mapper;


        public LocationDomain(ILocationRepository locationRepository, ILocationDtoFormatHelper createLocationDtoFormatHelper,
            IDomainLoggerExtension loggerExtension, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _CreateLocationDtoFormatHelper = createLocationDtoFormatHelper;
            _loggerExtension = loggerExtension;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Location>> GetLocation()
        {
            var locations = await _locationRepository.GetLocation();
            if (locations.Count() == 0)
                return null;

            return locations.OrderBy(x => x.Name);
        }

        public async Task<Location> GetLocation(int id)
        {
            return await _locationRepository.GetLocation(id);
        }

        public async Task<Location> CreateLocation(LocationDto locationDto)
        {
            locationDto = _CreateLocationDtoFormatHelper.FormatEntity(locationDto);
            if (locationDto == null)
                return null;

            var location = MapLocationAsCorrectType(locationDto);
            if (location == null)
                return null;

            if (await DoesLocationAlreadyExist(location))
                return null;

            return await _locationRepository.SaveLocation(location);
        }

        public async Task<bool> DeleteLocation(int id)
        {
            var location = await _locationRepository.GetLocation(id);
            if (location == null)
                return false;

            return await _locationRepository.DeleteLocation(location);
        }

        private Location MapLocationAsCorrectType(LocationDto locationDto)
        {
            try
            {
                switch (locationDto.LocationId)
                {
                    case LocationType.City:
                        return _mapper.Map<City>(locationDto);
                    case LocationType.Town:
                        return _mapper.Map<Town>(locationDto);
                    case LocationType.Homestead:
                        return _mapper.Map<Homestead>(locationDto);
                    case LocationType.Settlement:
                        return _mapper.Map<Settlement>(locationDto);
                    case LocationType.DaedricShrine:
                        return _mapper.Map<DaedricShrine>(locationDto);
                    case LocationType.StandingStone:
                        return _mapper.Map<StandingStone>(locationDto);
                    case LocationType.Landmark:
                        return _mapper.Map<Landmark>(locationDto);
                    case LocationType.Camp:
                        return _mapper.Map<Camp>(locationDto);
                    case LocationType.Cave:
                        return _mapper.Map<Cave>(locationDto);
                    case LocationType.Clearing:
                        return _mapper.Map<Clearing>(locationDto);
                    case LocationType.Dock:
                        return _mapper.Map<Dock>(locationDto);
                    case LocationType.DragonLair:
                        return _mapper.Map<DragonLair>(locationDto);
                    case LocationType.DwarvenRuin:
                        return _mapper.Map<DwarvenRuin>(locationDto);
                    case LocationType.Farm:
                        return _mapper.Map<Farm>(locationDto);
                    case LocationType.Fort:
                        return _mapper.Map<Fort>(locationDto);
                    case LocationType.GiantCamp:
                        return _mapper.Map<GiantCamp>(locationDto);
                    case LocationType.Grove:
                        return _mapper.Map<Grove>(locationDto);
                    case LocationType.ImperialCamp:
                        return _mapper.Map<ImperialCamp>(locationDto);
                    case LocationType.LightHouse:
                        return _mapper.Map<LightHouse>(locationDto);
                    case LocationType.Mine:
                        return _mapper.Map<Mine>(locationDto);
                    case LocationType.NordicTower:
                        return _mapper.Map<NordicTower>(locationDto);
                    case LocationType.OrcStronghold:
                        return _mapper.Map<OrcStronghold>(locationDto);
                    case LocationType.Pass:
                        return _mapper.Map<Pass>(locationDto);
                    case LocationType.Ruin:
                        return _mapper.Map<Ruin>(locationDto);
                    case LocationType.Shack:
                        return _mapper.Map<Shack>(locationDto);
                    case LocationType.Ship:
                        return _mapper.Map<Ship>(locationDto);
                    case LocationType.Shipwreck:
                        return _mapper.Map<Shipwreck>(locationDto);
                    case LocationType.Stable:
                        return _mapper.Map<Stable>(locationDto);
                    case LocationType.StormcloakCamp:
                        return _mapper.Map<StormcloakCamp>(locationDto);
                    case LocationType.Tomb:
                        return _mapper.Map<Tomb>(locationDto);
                    case LocationType.Watchtower:
                        return _mapper.Map<Watchtower>(locationDto);
                    case LocationType.WheatMill:
                        return _mapper.Map<WheatMill>(locationDto);
                    case LocationType.LumberMill:
                        return _mapper.Map<LumberMill>(locationDto);
                    case LocationType.BodyOfWater:
                        return _mapper.Map<BodyOfWater>(locationDto);
                    case LocationType.InnOrTavern:
                        return _mapper.Map<InnOrTavern>(locationDto);
                    case LocationType.Temple:
                        return _mapper.Map<Temple>(locationDto);
                    case LocationType.WordWall:
                        return _mapper.Map<WordWall>(locationDto);
                    case LocationType.Castle:
                        return _mapper.Map<Castle>(locationDto);
                    case LocationType.GuildHeadquarter:
                        return _mapper.Map<GuildHeadquarter>(locationDto);
                    case LocationType.UnmarkedLocation:
                        return _mapper.Map<UnmarkedLocation>(locationDto);
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e, locationDto);

                return null;
            }
        }

        private async Task<bool> DoesLocationAlreadyExist(Location location)
        {
            var existingLocations = await _locationRepository.GetLocation();
            var existingLocation = existingLocations.FirstOrDefault(x =>
            x.LocationId == location.LocationId &&
            x.Name == location.Name);
            if (existingLocation == null)
                return false;

            return true;
        }
    }
}
