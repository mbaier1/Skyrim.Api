using Microsoft.EntityFrameworkCore;
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
        public DbSet<ImperialCamp> ImperialCamps { get; set; }
        public DbSet<LightHouse> LightHouses { get; set; }
        public DbSet<Mine> Mines { get; set; }
        public DbSet<NordicTower> NordicTowers { get; set; }
        public DbSet<OrcStronghold> OrcStrongholds { get; set; }
        public DbSet<Pass> Passes { get; set; }
        public DbSet<Ruin> Ruins { get; set; }
        public DbSet<Shack> Shacks { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Shipwreck> Shipwrecks { get; set; }
        public DbSet<Stable> Stables { get; set; }
        public DbSet<StormcloakCamp> StormcloakCamps { get; set; }
        public DbSet<Tomb> Tombs { get; set; }
        public DbSet<Watchtower> Watchtowers { get; set; }
        public DbSet<WheatMill> WheatMills { get; set; }
        public DbSet<LumberMill> LumberMills { get; set; }
        public DbSet<BodyOfWater> BodiesOfWater { get; set; }
        public DbSet<InnOrTavern> InnsOrTaverns { get; set; }
        public DbSet<Temple> Temples { get; set; }
        public DbSet<WordWall> WordWalls { get; set; }
        public DbSet<Castle> Castles { get; set; }
        public DbSet<GuildHeadquarter> GuildHeadquarters { get; set; }

        public DbSet<Guard> Guards { get; set; }
        public DbSet<PhyscialFightingShop> PhyscialFightingShops { get; set; }
        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}
