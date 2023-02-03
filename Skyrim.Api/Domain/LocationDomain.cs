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

        public LocationDomain(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> CreateLocation(CreateLocationDto createLocationDto)
        {
            createLocationDto = VerifyCreateLocationDtoHasValidProperties(createLocationDto);
            if (createLocationDto == null)
                return null;

            return await CreateLocationAsCorrectLocationType(createLocationDto);
        }

        private CreateLocationDto VerifyCreateLocationDtoHasValidProperties(CreateLocationDto createLocationDto)
        {
            createLocationDto = CheckForNullOrWhiteSpaceProperties(createLocationDto);
            if (createLocationDto == null)
                return null;

            createLocationDto = TrimWhiteSpace(createLocationDto);

            return createLocationDto;
        }

        private CreateLocationDto CheckForNullOrWhiteSpaceProperties(CreateLocationDto createLocationDto)
        {
            if (string.IsNullOrWhiteSpace(createLocationDto.Description))
                createLocationDto.Description = "";

            if (string.IsNullOrWhiteSpace(createLocationDto.Name)
                || string.IsNullOrWhiteSpace(createLocationDto.GeographicalDescription))
                return null;

            return createLocationDto;
        }

        private CreateLocationDto TrimWhiteSpace(CreateLocationDto createLocationDto)
        {
            createLocationDto.Name = createLocationDto.Name.Trim();
            createLocationDto.Description = createLocationDto.Description.Trim();
            createLocationDto.GeographicalDescription = createLocationDto.GeographicalDescription.Trim();

            return createLocationDto;
        }

        private async Task<Location> CreateLocationAsCorrectLocationType(CreateLocationDto createLocationDto)
        {
            switch (createLocationDto.TypeOfLocation)
            {
                case LocationType.City:
                    return await _locationRepository.SaveLocationAsCity(createLocationDto);
                default:
                    return null;
            }
        }
    }
}
