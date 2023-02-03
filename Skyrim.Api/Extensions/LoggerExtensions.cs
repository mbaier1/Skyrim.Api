using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;

namespace Skyrim.Api.Extensions
{
    public class LoggerExtensions : ILoggerExtension
    {
        private readonly ILogger<LocationRepository> _LocationRepositoryLogger;

        public LoggerExtensions(ILogger<LocationRepository> locationRepositoryLogger)
        {
            _LocationRepositoryLogger = locationRepositoryLogger;
        }

        public void LogFatalError(Exception e, CreateLocationDto createLocationDto)
        {
            _LocationRepositoryLogger.Log(LogLevel.Error, e.Message, createLocationDto);
        }
    }
}
