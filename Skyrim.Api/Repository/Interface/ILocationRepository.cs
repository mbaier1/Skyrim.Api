using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Repository.Interface
{
    public interface ILocationRepository
    {
        Task<Location> SaveLocation(Location location);
        //Task<Location> SaveLocationAsCity(CreateLocationDto createLocationDto);
        //Task<Location> SaveLocationAsTown(CreateLocationDto createLocationDto);
        //Task<Location> SaveLocationAsHomestead(CreateLocationDto createLocationDto);
        //Task<Location> SaveLocationAsSettlement(CreateLocationDto createLocationDto);
        //Task<Location> SaveLocationAsDaedricShrine(CreateLocationDto createLocationDto);
    }
}
