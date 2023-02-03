using Microsoft.EntityFrameworkCore;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Models;
using System;

namespace Skyrim.Api.Data.Interfaces
{
    public interface ISkyrimApiDbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<PhyscialFightingShop> PhyscialFightingShops { get; set; }
        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}
