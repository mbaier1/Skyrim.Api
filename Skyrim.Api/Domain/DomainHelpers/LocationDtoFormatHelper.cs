using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Skyrim.Api.Domain.DomainHelpers
{
    public class LocationDtoFormatHelper : GenericFormatHelper<LocationDto>, ILocationDtoFormatHelper
    {
        public LocationDto FormatEntity(LocationDto locationDto)
        {
            locationDto = CheckForNullOrWhiteSpaceProperties(locationDto);
            if (locationDto == null)
                return null;

            locationDto = TrimWhiteSpaceProperties(locationDto);
            locationDto.Name = FormatNameCorrectly(locationDto.Name);

            return locationDto;
        }

        private LocationDto CheckForNullOrWhiteSpaceProperties(LocationDto locationDto)
        {
            if (string.IsNullOrWhiteSpace(locationDto.Description))
                locationDto.Description = "";

            if (string.IsNullOrWhiteSpace(locationDto.Name)
                || string.IsNullOrWhiteSpace(locationDto.GeographicalDescription))
                return null;

            return locationDto;
        }

        private LocationDto TrimWhiteSpaceProperties(LocationDto locationDto)
        {
            locationDto.Name = locationDto.Name.Trim();
            locationDto.Description = locationDto.Description.Trim();
            locationDto.GeographicalDescription = locationDto.GeographicalDescription.Trim();

            return locationDto;
        }

        private string FormatNameCorrectly(string name)
        {
            return Regex.Replace(name.ToLower(), @"\b\w", (Match match) => match.ToString().ToUpper());
        }
    }
}
