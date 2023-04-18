using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Domain.Interfaces
{
    public interface ILocationDtoFormatHelper : IGenericFormatHelper<LocationDto>
    {
        LocationDto FormatEntity(LocationDto locationDto);
    }
}
