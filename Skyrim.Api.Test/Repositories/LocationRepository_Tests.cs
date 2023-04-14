using Microsoft.EntityFrameworkCore;
using Moq;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Test.TestHelpers;

namespace Skyrim.Api.Test.Repositories
{
    public class LocationRepository_Tests
    {
        protected ILocationRepository _locationRepository;
        protected SkyrimApiDbContext _context;
        protected Mock<IRepositoryLoggerExtension> _mockLoggerExtension;

        protected ILocationRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<SkyrimApiDbContext>()
            .UseInMemoryDatabase(databaseName: "MockSkyrimLocationDb")
            .Options;

            _context = new SkyrimApiDbContext(options);
            _mockLoggerExtension = new Mock<IRepositoryLoggerExtension>();

            return _locationRepository = new LocationRepository(_context, _mockLoggerExtension.Object);
        }

        public LocationRepository_Tests()
        {
            _locationRepository = GetInMemoryRepository();
        }
    }

    public class SaveLocation : LocationRepository_Tests
    {
        [Theory]
        [MemberData(nameof(ValidLocationForEachLocationType))]
        public async void WithValidCreateLocationDto_SavesExpectedLocation(string description, Location location)
        {
            // Arrange

            // Act
            var result = await _locationRepository.SaveLocation(location);

            // Assert
            switch (location.TypeOfLocation)
            {
                case LocationType.City:
                    Assert.Equal(_context.Cities.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Town:
                    Assert.Equal(_context.Towns.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Homestead:
                    Assert.Equal(_context.Homesteads.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Settlement:
                    Assert.Equal(_context.Settlements.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.DaedricShrine:
                    Assert.Equal(_context.DaedricShrines.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.StandingStone:
                    Assert.Equal(_context.StandingStones.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Landmark:
                    Assert.Equal(_context.Landmarks.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Camp:
                    Assert.Equal(_context.Camps.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Cave:
                    Assert.Equal(_context.Caves.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Clearing:
                    Assert.Equal(_context.Clearings.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Dock:
                    Assert.Equal(_context.Docks.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.DragonLair:
                    Assert.Equal(_context.DragonLairs.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.DwarvenRuin:
                    Assert.Equal(_context.DwarvenRuins.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Farm:
                    Assert.Equal(_context.Farms.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Fort:
                    Assert.Equal(_context.Forts.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.GiantCamp:
                    Assert.Equal(_context.GiantCamps.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Grove:
                    Assert.Equal(_context.Groves.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.ImperialCamp:
                    Assert.Equal(_context.ImperialCamps.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.LightHouse:
                    Assert.Equal(_context.LightHouses.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Mine:
                    Assert.Equal(_context.Mines.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.NordicTower:
                    Assert.Equal(_context.NordicTowers.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.OrcStronghold:
                    Assert.Equal(_context.OrcStrongholds.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Pass:
                    Assert.Equal(_context.Passes.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Ruin:
                    Assert.Equal(_context.Ruins.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Shack:
                    Assert.Equal(_context.Shacks.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Ship:
                    Assert.Equal(_context.Ships.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Shipwreck:
                    Assert.Equal(_context.Shipwrecks.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Stable:
                    Assert.Equal(_context.Stables.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.StormcloakCamp:
                    Assert.Equal(_context.StormcloakCamps.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Tomb:
                    Assert.Equal(_context.Tombs.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Watchtower:
                    Assert.Equal(_context.Watchtowers.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.WheatMill:
                    Assert.Equal(_context.WheatMills.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.LumberMill:
                    Assert.Equal(_context.LumberMills.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.BodyOfWater:
                    Assert.Equal(_context.BodiesOfWater.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.InnOrTavern:
                    Assert.Equal(_context.InnsOrTaverns.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Temple:
                    Assert.Equal(_context.Temples.FirstOrDefault().Name, result.Name);
                    break;
                default:
                    Assert.True(false);
                    break;
            }
        }
        public static IEnumerable<object[]> ValidLocationForEachLocationType()
        {
            yield return new object[]
            {
                "Valid properties for a City",
                TestMethodHelpers.CreateNewCity()
            };
            yield return new object[]
            {
                "Valid properties for a Town",
                TestMethodHelpers.CreateNewTown()
            };
            yield return new object[]
            {
                "Valid properties for a Homestead",
                TestMethodHelpers.CreateNewHomestead()
            };
            yield return new object[]
            {
                "Valid properties for a Settlement",
                TestMethodHelpers.CreateNewSettlement()
            };
            yield return new object[]
            {
                "Valid properties for a DaedricShrine",
                TestMethodHelpers.CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                "Valid properties for a StandingStone",
                TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                "Valid properties for a Landmark",
                TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                "Valid properties for a Camp",
                TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                "Valid properties for a Cave",
                TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                "Valid properties for a Clearing",
                TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                "Valid properties for a Dock",
                TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                "Valid properties for a DragonLair",
                TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                "Valid properties for a DwarvenRuin",
                TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                "Valid properties for a Farm",
                TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                "Valid properties for a Fort",
                TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                "Valid properties for a GiantCamp",
                TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                "Valid properties for a Grove",
                TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                "Valid properties for a ImperialCamp",
                TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                "Valid properties for a LightHouse",
                TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                "Valid properties for a Mine",
                TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                "Valid properties for a NordicTower",
                TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                "Valid properties for a OrcStronghold",
                TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                "Valid properties for a Pass",
                TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                "Valid properties for a Ruin",
                TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                "Valid properties for a Shack",
                TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                "Valid properties for a Ship",
                TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                "Valid properties for a Shipwreck",
                TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                "Valid properties for a Stable",
                TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                "Valid properties for a StormcloakCamp",
                TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                "Valid properties for a Tomb",
                TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                "Valid properties for a Watchtower",
                TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                "Valid properties for a WheatMill",
                TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                "Valid properties for a LumberMill",
                TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                "Valid properties for a BodyOfWater",
                TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                "Valid properties for a InnOrTavern",
                TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                "Valid properties for a Temple",
                TestMethodHelpers.CreateNewTemple()
            };
        }

        [Theory]
        [MemberData(nameof(ValidLocationForEachLocationType))]
        public async void WithValidLocations_ReturnsExpectedLocation(string description, Location location)
        {
            // Arrange

            // Act
            var result = await _locationRepository.SaveLocation(location);

            // Assert
            Assert.Equal(location.Name, result.Name);
            Assert.Equal(location.Description, result.Description);
            Assert.Equal(location.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(location.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(location.Id, result.Id);
        }

        [Theory]
        [MemberData(nameof(InvalidPropertiesForEachLocationType))]
        public async void WithInvalidLocations_LogsExpectedError(string description, Location location)
        {
            //Arrange

            // Act
            await _locationRepository.SaveLocation(location);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<Location>()), Times.Once);
        }
        public static IEnumerable<object[]> InvalidPropertiesForEachLocationType()
        {
            yield return new object[]
            {
                "Invalid properties for City",
                new City()
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                new Town()
            };
            yield return new object[]
            {
                "Invalid properties for Settlment",
                new Settlement()
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                new Homestead()
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                new DaedricShrine()
            };
            yield return new object[]
            {
                "Invalid properties for StandingStone",
                new StandingStone()
            };
            yield return new object[]
            {
                "Invalid properties for Landmark",
                new Landmark()
            };
            yield return new object[]
            {
                "Invalid properties for Camp",
                new Camp()
            };
            yield return new object[]
            {
                "Invalid properties for Cave",
                new Cave()
            };
            yield return new object[]
            {
                "Invalid properties for Clearing",
                new Clearing()
            };
            yield return new object[]
            {
                "Invalid properties for Dock",
                new Dock()
            };
            yield return new object[]
            {
                "Invalid properties for DragonLair",
                new DragonLair()
            };
            yield return new object[]
            {
                "Invalid properties for DwarvenRuin",
                new DwarvenRuin()
            };
            yield return new object[]
            {
                "Invalid properties for Farm",
                new Farm()
            };
            yield return new object[]
            {
                "Invalid properties for Fort",
                new Fort()
            };
            yield return new object[]
            {
                "Invalid properties for GiantCamp",
                new GiantCamp()
            };
            yield return new object[]
            {
                "Invalid properties for Grove",
                new Grove()
            };
            yield return new object[]
            {
                "Invalid properties for ImperialCamp",
                new ImperialCamp()
            };
            yield return new object[]
            {
                "Invalid properties for LightHouse",
                new LightHouse()
            };
            yield return new object[]
            {
                "Invalid properties for Mine",
                new Mine()
            };
            yield return new object[]
            {
                "Invalid properties for NordicTower",
                new NordicTower()
            };
            yield return new object[]
            {
                "Invalid properties for OrcStronghold",
                new OrcStronghold()
            };
            yield return new object[]
            {
                "Invalid properties for Pass",
                new Pass()
            };
            yield return new object[]
            {
                "Invalid properties for Ruin",
                new Ruin()
            };
            yield return new object[]
            {
                "Invalid properties for Shack",
                new Shack()
            };
            yield return new object[]
            {
                "Invalid properties for Ship",
                new Ship()
            };
            yield return new object[]
            {
                "Invalid properties for Shipwreck",
                new Shipwreck()
            };
            yield return new object[]
            {
                "Invalid properties for Stable",
                new Stable()
            };
            yield return new object[]
            {
                "Invalid properties for StormcloakCamp",
                new StormcloakCamp()
            };
            yield return new object[]
            {
                "Invalid properties for Tomb",
                new Tomb()
            };
            yield return new object[]
            {
                "Invalid properties for Watchtower",
                new Watchtower()
            };
            yield return new object[]
            {
                "Invalid properties for WheatMill",
                new WheatMill()
            };
            yield return new object[]
            {
                "Invalid properties for LumberMill",
                new LumberMill()
            };
            yield return new object[]
            {
                "Invalid properties for BodyOfWater",
                new BodyOfWater()
            };
            yield return new object[]
            {
                "Invalid properties for InnOrTavern",
                new InnOrTavern()
            };
            yield return new object[]
            {
                "Invalid properties for Temple",
                new Temple()
            };
        }

        [Theory]
        [MemberData(nameof(InvalidPropertiesForEachLocationType))]
        public async void WithInvalidLocations_ReturnsExpectedNull(string description, Location location)
        {
            // Arrange

            // Act
            var result = await _locationRepository.SaveLocation(location);

            // Assert
            Assert.Equal(null, result);
        }
    }
}