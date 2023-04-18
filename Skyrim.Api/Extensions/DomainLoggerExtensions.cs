using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;

namespace Skyrim.Api.Extensions
{
    public class DomainLoggerExtensions : IDomainLoggerExtension
    {
        private readonly ILogger<LocationDomain> _locationDtoDomainLogger;

        public DomainLoggerExtensions(ILogger<LocationDomain> locationDtoDomainLogger)
        {
            _locationDtoDomainLogger = locationDtoDomainLogger;
        }

        public void LogError(Exception e, LocationDto locationDto)
        {
            _locationDtoDomainLogger.Log(LogLevel.Error, e.Message, locationDto);
        }
    }
}
