using Microsoft.EntityFrameworkCore;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Interfaces;
using Skyrim.Api.Data.Models;
using System;

namespace Skyrim.Api.Data
{
    public class SkyrimApiDbContext : DbContext, ISkyrimApiDbContext
    {
        public SkyrimApiDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().UseTpcMappingStrategy();
            modelBuilder.Entity<Building>().UseTpcMappingStrategy();
            modelBuilder.Entity<Person>().UseTpcMappingStrategy();
            modelBuilder.Entity<Creature>().UseTpcMappingStrategy();
            modelBuilder.Entity<Shop>().UseTpcMappingStrategy();
            modelBuilder.Entity<Patroller>().UseTpcMappingStrategy();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<PhyscialFightingShop> PhyscialFightingShops { get; set; }
        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}
