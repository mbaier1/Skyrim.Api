using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;

namespace Skyrim.Api.Extensions
{
    public class RepositoryLoggerExtensions : IRepositoryLoggerExtension
    {
        private readonly ILogger<LocationRepository> _LocationRepositoryLogger;

        public RepositoryLoggerExtensions(ILogger<LocationRepository> locationRepositoryLogger)
        {
            _LocationRepositoryLogger = locationRepositoryLogger;
        }

        public void LogError(Exception e, Location location=null)
        {
            _LocationRepositoryLogger.Log(LogLevel.Error, e.Message, location);
        }
    }
}
