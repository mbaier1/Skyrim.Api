using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;
using Location = Skyrim.Api.Data.AbstractModels.Location;

namespace Skyrim.Api.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SkyrimApiDbContext _context;
        private readonly IRepositoryLoggerExtension _loggerExtension;

        public LocationRepository(SkyrimApiDbContext context, IRepositoryLoggerExtension loggerExtension)
        {
            _context = context;
            _loggerExtension = loggerExtension;
        }

        public async Task<IEnumerable<Location>> GetLocation()
        {
            try
            {
                return await _context.Location.Where(x => x.Id >= 1).ToArrayAsync();
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e);
            }

            return null;
        }

        public async Task<Location> GetLocation(int id)
        {
            try
            {
                return await _context.Location.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e);
            }

            return null;
        }

        public async Task<Location> UpdateLocation(Location updatedLocation)
        {
            try
            {
                var existingLocation = await _context.Location.FirstAsync(x => x.Id == updatedLocation.Id);
                existingLocation.LocationId = updatedLocation.LocationId;
                existingLocation.TypeOfLocation = updatedLocation.LocationId.GetDisplayName();
                existingLocation.Name = updatedLocation.Name;
                existingLocation.Description = updatedLocation.Description;
                existingLocation.GeographicalDescription = updatedLocation.GeographicalDescription;
                _context.Location.Update(existingLocation);
                await _context.SaveChangesAsync();

                return updatedLocation;
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e, updatedLocation);
            }

            return null;
        }

        public async Task<Location> SaveLocation(Location location)
        {
            try
            {
                await _context.AddAsync(location);
                await _context.SaveChangesAsync();

                return location;
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e, location);
            }

            return null;
        }

        public async Task<bool> DeleteLocation(Location location)
        {
            try
            {
                _context.Location.Remove(location);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _loggerExtension.LogError(e);
            }

            return false;
        }
    }
}
