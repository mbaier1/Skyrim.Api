using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;

namespace Skyrim.Api.Extensions
{
    public class DomainLoggerExtensions : IDomainLoggerExtension
    {
        private readonly ILogger<LocationDomain> _creatLocationDtoDomainLogger;

        public DomainLoggerExtensions(ILogger<LocationDomain> creatLocationDtoDomainLogger)
        {
            _creatLocationDtoDomainLogger = creatLocationDtoDomainLogger;
        }

        public void LogError(Exception e, LocationDto createLocationDto)
        {
            _creatLocationDtoDomainLogger.Log(LogLevel.Error, e.Message, createLocationDto);
        }
    }
}
