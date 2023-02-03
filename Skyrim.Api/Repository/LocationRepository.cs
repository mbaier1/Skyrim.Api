using AutoMapper;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Data;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SkyrimApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerExtension _loggerExtension;

        public LocationRepository(SkyrimApiDbContext context, IMapper mapper, ILoggerExtension loggerExtension)
        {
            _context = context;
            _mapper = mapper;
            _loggerExtension = loggerExtension;
        }

        public async Task<Location> SaveLocationAsCity(CreateLocationDto createLocationDto)
        {
            try
            {
                var city = _mapper.Map<City>(createLocationDto);
                await _context.AddAsync(city);
                await _context.SaveChangesAsync();

                return city;
            }
            catch (Exception e)
            {
                _loggerExtension.LogFatalError(e, createLocationDto);

                return null;
            }
        }
    }
}
