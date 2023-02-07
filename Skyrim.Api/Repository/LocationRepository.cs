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
        private readonly IRepositoryLoggerExtension _loggerExtension;

        public LocationRepository(SkyrimApiDbContext context, IRepositoryLoggerExtension loggerExtension)
        {
            _context = context;
            _loggerExtension = loggerExtension;
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

        //public async Task<Location> SaveLocationAsCity(CreateLocationDto createLocationDto)
        //{
        //    try
        //    {
        //        var city = _mapper.Map<City>(createLocationDto);
        //        await _context.AddAsync(city);
        //        await _context.SaveChangesAsync();

        //        return city;
        //    }
        //    catch (Exception e)
        //    {
        //        _loggerExtension.LogError(e, createLocationDto);

        //        return null;
        //    }
        //}
        //public async Task<Location> SaveLocationAsTown(CreateLocationDto createLocationDto)
        //{
        //    try
        //    {
        //        var town = _mapper.Map<Town>(createLocationDto);
        //        await _context.AddAsync(town);
        //        await _context.SaveChangesAsync();

        //        return town;
        //    }
        //    catch (Exception e)
        //    {
        //        _loggerExtension.LogError(e, createLocationDto);

        //        return null;
        //    }
        //}
        //public async Task<Location> SaveLocationAsHomestead(CreateLocationDto createLocationDto)
        //{
        //    try
        //    {
        //        var homestead = _mapper.Map<Homestead>(createLocationDto);
        //        await _context.AddAsync(homestead);
        //        await _context.SaveChangesAsync();

        //        return homestead;
        //    }
        //    catch (Exception e)
        //    {
        //        _loggerExtension.LogError(e, createLocationDto);

        //        return null;
        //    }
        //}
        //public async Task<Location> SaveLocationAsSettlement(CreateLocationDto createLocationDto)
        //{
        //    try
        //    {
        //        var settlement = _mapper.Map<Settlement>(createLocationDto);
        //        await _context.AddAsync(settlement);
        //        await _context.SaveChangesAsync();

        //        return settlement;
        //    }
        //    catch (Exception e)
        //    {
        //        _loggerExtension.LogError(e, createLocationDto);

        //        return null;
        //    }
        //}
        //public async Task<Location> SaveLocationAsDaedricShrine(CreateLocationDto createLocationDto)
        //{
        //    try
        //    {
        //        var daedricShrine = _mapper.Map<DaedricShrine>(createLocationDto);
        //        await _context.AddAsync(daedricShrine);
        //        await _context.SaveChangesAsync();

        //        return daedricShrine;
        //    }
        //    catch (Exception e)
        //    {
        //        _loggerExtension.LogError(e, createLocationDto);

        //        return null;
        //    }
        //}
    }
}
