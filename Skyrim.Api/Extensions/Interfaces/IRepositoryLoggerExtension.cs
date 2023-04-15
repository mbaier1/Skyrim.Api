using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Extensions.Interfaces
{
    public interface IRepositoryLoggerExtension : IGenericLoggerExtension<Location>
    {
        void LogError(Exception e, Location location=null);
    }
}
