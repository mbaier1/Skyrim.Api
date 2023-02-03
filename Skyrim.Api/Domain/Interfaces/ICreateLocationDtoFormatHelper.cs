using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Domain.Interfaces
{
    public interface ICreateLocationDtoFormatHelper : IGenericFormatHelper<CreateLocationDto>
    {
        CreateLocationDto FormatEntity(CreateLocationDto createLocationDto);
    }
}
