﻿using AutoMapper;
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
        private readonly ICreateLocationDtoFormatHelper _CreateLocationDtoFormatHelper;
        private readonly IDomainLoggerExtension _loggerExtension;
        private readonly IMapper _mapper;


        public LocationDomain(ILocationRepository locationRepository, ICreateLocationDtoFormatHelper createLocationDtoFormatHelper,
            IDomainLoggerExtension loggerExtension, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _CreateLocationDtoFormatHelper = createLocationDtoFormatHelper;
            _loggerExtension = loggerExtension;
            _mapper = mapper;
        }

        public async Task<Location> CreateLocation(CreateLocationDto createLocationDto)
        {
            createLocationDto = _CreateLocationDtoFormatHelper.FormatEntity(createLocationDto);
            if (createLocationDto == null)
                return null;

            var location = MapLocationAsCorrectType(createLocationDto);
            if (location == null)
                return null;

            return await _locationRepository.SaveLocation(location);
        }

        private Location MapLocationAsCorrectType(CreateLocationDto createLocationDto)
        {
            try
            {
                switch (createLocationDto.TypeOfLocation)
                {
                    case LocationType.City:
                        return _mapper.Map<City>(createLocationDto);
                    case LocationType.Town:
                        return _mapper.Map<Town>(createLocationDto);
                    case LocationType.Homestead:
                        return _mapper.Map<Homestead>(createLocationDto);
                    case LocationType.Settlement:
                        return _mapper.Map<Settlement>(createLocationDto);
                    case LocationType.DaedricShrine:
                        return _mapper.Map<DaedricShrine>(createLocationDto);
                    case LocationType.StandingStone:
                        return _mapper.Map<StandingStone>(createLocationDto);
                    case LocationType.Landmark:
                        return _mapper.Map<Landmark>(createLocationDto);
                    case LocationType.Camp:
                        return _mapper.Map<Camp>(createLocationDto);
                    case LocationType.Cave:
                        return _mapper.Map<Cave>(createLocationDto);
                    case LocationType.Clearing:
                        return _mapper.Map<Clearing>(createLocationDto);
                    case LocationType.Dock:
                        return _mapper.Map<Dock>(createLocationDto);
                    case LocationType.DragonLair:
                        return _mapper.Map<DragonLair>(createLocationDto);
                    case LocationType.DwarvenRuin:
                        return _mapper.Map<DwarvenRuin>(createLocationDto);
                    case LocationType.Farm:
                        return _mapper.Map<Farm>(createLocationDto);
                    case LocationType.Fort:
                        return _mapper.Map<Fort>(createLocationDto);
                    case LocationType.GiantCamp:
                        return _mapper.Map<GiantCamp>(createLocationDto);
                    case LocationType.Grove:
                        return _mapper.Map<Grove>(createLocationDto);
                    case LocationType.ImperialCamp:
                        return _mapper.Map<ImperialCamp>(createLocationDto);
                    case LocationType.LightHouse:
                        return _mapper.Map<LightHouse>(createLocationDto);
                    case LocationType.Mine:
                        return _mapper.Map<Mine>(createLocationDto);
                    case LocationType.NordicTower:
                        return _mapper.Map<NordicTower>(createLocationDto);
                    case LocationType.OrcStronghold:
                        return _mapper.Map<OrcStronghold>(createLocationDto);
                    case LocationType.Pass:
                        return _mapper.Map<Pass>(createLocationDto);
                    case LocationType.Ruin:
                        return _mapper.Map<Ruin>(createLocationDto);
                    case LocationType.Shack:
                        return _mapper.Map<Shack>(createLocationDto);
                    case LocationType.Ship:
                        return _mapper.Map<Ship>(createLocationDto);
                    case LocationType.Shipwreck:
                        return _mapper.Map<Shipwreck>(createLocationDto);
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e, createLocationDto);

                return null;
            }
        }
    }
}
