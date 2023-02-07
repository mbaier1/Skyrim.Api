using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;

namespace Skyrim.Api.Extensions
{
    public class DomainLoggerExtension : IDomainLoggerExtension
    {
        private readonly ILogger<LocationDomain> _creatLocationDtoDomainLogger;

        public DomainLoggerExtension(ILogger<LocationDomain> creatLocationDtoDomainLogger)
        {
            _creatLocationDtoDomainLogger = creatLocationDtoDomainLogger;
        }

        public void LogError(Exception e, CreateLocationDto createLocationDto)
        {
            _creatLocationDtoDomainLogger.Log(LogLevel.Error, e.Message, createLocationDto);
        }
    }
}
