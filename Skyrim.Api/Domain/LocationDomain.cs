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
