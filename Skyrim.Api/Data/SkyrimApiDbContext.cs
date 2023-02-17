﻿using Microsoft.EntityFrameworkCore;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Interfaces;
using Skyrim.Api.Data.Models;

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
        public DbSet<Homestead> Homesteads { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<DaedricShrine> DaedricShrines { get; set; }
        public DbSet<StandingStone> StandingStones { get; set; }
        public DbSet<Landmark> Landmarks { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Cave> Caves { get; set; }
        public DbSet<Clearing> Clearings { get; set; }
        public DbSet<Dock> Docks { get; set; }
        public DbSet<DragonLair> DragonLairs { get; set; }
        public DbSet<DwarvenRuin> DwarvenRuins { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Fort> Forts { get; set; }
        public DbSet<GiantCamp> GiantCamps { get; set; }
        public DbSet<Grove> Groves { get; set; }

        public DbSet<Guard> Guards { get; set; }
        public DbSet<PhyscialFightingShop> PhyscialFightingShops { get; set; }
        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}
