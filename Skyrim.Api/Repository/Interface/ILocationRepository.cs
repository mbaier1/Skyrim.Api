using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Repository.Interface
{
    public interface ILocationRepository
    {
        Task<Location> SaveLocation(Location location);
        Task<Location> GetLocation(int id);
    }
}
