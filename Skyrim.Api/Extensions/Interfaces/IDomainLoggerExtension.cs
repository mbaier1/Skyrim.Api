using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Extensions.Interfaces
{
    public interface IDomainLoggerExtension : IGenericLoggerExtension<CreateLocationDto>
    {
        void LogError(Exception e, CreateLocationDto createLocationDto);
    }
}
