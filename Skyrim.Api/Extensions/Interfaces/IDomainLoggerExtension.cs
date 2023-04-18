using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Extensions.Interfaces
{
    public interface IDomainLoggerExtension : IGenericLoggerExtension<LocationDto>
    {
        void LogError(Exception e, LocationDto locationDto);
    }
}
