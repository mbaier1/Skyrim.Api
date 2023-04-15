using Microsoft.EntityFrameworkCore;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;

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

                return null;
            }
        }
    }
}
