using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Domain.Interfaces
{
    public interface ILocationDomain
    {
        Task<IEnumerable<Location>> GetLocation();
        Task<Location> GetLocation(int id);
        Task<Location> CreateLocation(CreateLocationDto createLocationDto);
    }
}
