using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Repository.Interface
{
    public interface ILocationRepository
    {
        Task<Location> SaveLocationAsCity(CreateLocationDto createLocationDto);
        Task<Location> SaveLocationAsTown(CreateLocationDto createLocationDto);
    }
}
