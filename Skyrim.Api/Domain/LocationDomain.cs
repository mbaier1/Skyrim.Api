using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Repository.Interface;

namespace Skyrim.Api.Domain
{
    public class LocationDomain : ILocationDomain
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICreateLocationDtoFormatHelper _CreateLocationDtoFormatHelper;

        public LocationDomain(ILocationRepository locationRepository, ICreateLocationDtoFormatHelper createLocationDtoFormatHelper)
        {
            _locationRepository = locationRepository;
            _CreateLocationDtoFormatHelper = createLocationDtoFormatHelper;
        }

        public async Task<Location> CreateLocation(CreateLocationDto createLocationDto)
        {
            createLocationDto = _CreateLocationDtoFormatHelper.FormatEntity(createLocationDto);
            if (createLocationDto == null)
                return null;

            return await CreateLocationAsCorrectType(createLocationDto);
        }

        private async Task<Location> CreateLocationAsCorrectType(CreateLocationDto createLocationDto)
        {
            switch (createLocationDto.TypeOfLocation)
            {
                case LocationType.City:
                    return await _locationRepository.SaveLocationAsCity(createLocationDto);
                case LocationType.Town:
                    return await _locationRepository.SaveLocationAsTown(createLocationDto);
                case LocationType.Homestead:
                    return await _locationRepository.SaveLocationAsHomestead(createLocationDto);
                case LocationType.Settlement:
                    return await _locationRepository.SaveLocationAsSettlement(createLocationDto);
                default:
                    return null;
            }
        }
    }
}
