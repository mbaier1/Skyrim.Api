using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;

namespace Skyrim.Api.Domain.DomainHelpers
{
    public class CreateLocationDtoFormatHelper : GenericFormatHelper<CreateLocationDto>, ICreateLocationDtoFormatHelper
    {
        public CreateLocationDto FormatEntity(CreateLocationDto createLocationDto)
        {
            createLocationDto = CheckForNullOrWhiteSpaceProperties(createLocationDto);
            if (createLocationDto == null)
                return null;

            createLocationDto = TrimWhiteSpaceProperties(createLocationDto);

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

        private CreateLocationDto TrimWhiteSpaceProperties(CreateLocationDto createLocationDto)
        {
            createLocationDto.Name = createLocationDto.Name.Trim();
            createLocationDto.Description = createLocationDto.Description.Trim();
            createLocationDto.GeographicalDescription = createLocationDto.GeographicalDescription.Trim();

            return createLocationDto;
        }
    }
}
