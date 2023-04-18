using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Repository.Interface
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocation();
        Task<Location> GetLocation(int id);
        Task<Location> UpdateLocation(Location location);
        Task<Location> SaveLocation(Location location);
        Task<bool> DeleteLocation(Location location);
    }
}
