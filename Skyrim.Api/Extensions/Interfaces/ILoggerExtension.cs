using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Extensions.Interfaces
{
    public interface ILoggerExtension
    {
        void LogFatalError(Exception e, CreateLocationDto createLocationDto);
    }
}
