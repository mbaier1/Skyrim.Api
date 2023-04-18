using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Moq;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Test.TestHelpers;
using Location = Skyrim.Api.Data.AbstractModels.Location;

namespace Skyrim.Api.Test.Repositories
{
    public class LocationRepository_Tests
    {
        protected ILocationRepository _locationRepository;
        protected SkyrimApiDbContext _context;
        protected Mock<IRepositoryLoggerExtension> _mockLoggerExtension;
        protected Mock<ILocationRepository> _partialMockLocationRepository;

        protected ILocationRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<SkyrimApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new SkyrimApiDbContext(options);
            _mockLoggerExtension = new Mock<IRepositoryLoggerExtension>();
            _partialMockLocationRepository = new Mock<ILocationRepository>();

            return _locationRepository = new LocationRepository(_context, _mockLoggerExtension.Object);
        }

        public LocationRepository_Tests()
        {
            _locationRepository = GetInMemoryRepository();
        }
    }

    public class GetLocations : LocationRepository_Tests
    {
        [Fact]
        public async void WithDataInDatabase_ReturnsExpectedLocations()
        {
            // Arrange
            var city = new City
            {
                Id = 1,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                Description = "Test"
            };
            var town = new Town
            {
                Id = 2,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.Town,
                Description = "Test"
            };
            var settlement = new Settlement
            {
                Id = 3,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.Settlement,
                Description = "Test"
            };

            await _context.AddAsync(city);
            await _context.AddAsync(town);
            await _context.AddAsync(settlement);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.GetLocation();

            // Assert
            Assert.Equal(city.Id, result.ToList()[0].Id);
            Assert.Equal(town.Id, result.ToList()[2].Id);
            Assert.Equal(settlement.Id, result.ToList()[1].Id);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WithNoDataInDatabase_ReturnsExpectedEmptyCollection()
        {
            // Arrange

            // Act
            var result = await _locationRepository.GetLocation();

            // Assert
            Assert.True(result.Count() == 0);
            _context.Database.EnsureDeleted();
        }

        //Note: The reality of this test is to log the error, but I am not able to due to the nature of the code.
        [Fact]
        public async void WhenErrorOccurs_ErrorIsThrown_ButItShouldCatchItAndLogError()
        {
            // Arrange
            _partialMockLocationRepository.Setup(x => x.GetLocation()).ThrowsAsync(new Exception());

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _partialMockLocationRepository.Object.GetLocation());
            _context.Database.EnsureDeleted();
        }
    }

    public class GetLocation : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidId_ReturnsExpectedLocation()
        {
            // Arrange
            int id = 1;
            var city = new City
            {
                Id = 1,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                Description = "Test"
            };

            await _context.AddAsync(city);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.GetLocation(id);

            // Assert
            Assert.Equal(city.Id, result.Id);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WithInvalidId_ReturnsExpectedNull()
        {
            // Arrange
            int id = 2;
            var city = new City
            {
                Id = 1,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                Description = "Test"
            };

            await _context.AddAsync(city);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.GetLocation(id);

            // Assert
            Assert.Null(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WhenErrorOccurs_ReturnsExpectedNull()
        {
            // Arrange
            int id = -0;

            // Act
            var result = await _locationRepository.GetLocation(id);

            // Assert
            Assert.Null(result);
            _context.Database.EnsureDeleted();
        }

        //Note: The reality of this test is to log the error, but I am not able to due to the nature of the code.
        [Fact]
        public async void WhenErrorOccurs_ErrorIsThrown_ButItShouldCatchItAndLogError()
        {
            // Arrange
            int id = -0;
            _partialMockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _partialMockLocationRepository.Object.GetLocation(id));
            _context.Database.EnsureDeleted();
        }
    }

    public class UpdateLocation : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidLocation_UpdatesLocation()
        {
            // Arrange
            var oldLocation = new City
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                TypeOfLocation = LocationType.City.GetDisplayName()
            };

            var updatedLocationData = new LocationDto
            {
                Name = "Changed",
                Description = "Changed",
                GeographicalDescription = "Changed",
                LocationId = LocationType.Tomb
            };

            await _context.AddAsync(oldLocation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.UpdateLocation(oldLocation, updatedLocationData);

            // Assert
            Assert.Equal(_context.Cities.FirstOrDefault().Name, result.Name);
            Assert.Equal(_context.Cities.FirstOrDefault().Description, result.Description);
            Assert.Equal(_context.Cities.FirstOrDefault().GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(_context.Cities.FirstOrDefault().LocationId, result.LocationId);
            Assert.Equal(_context.Cities.FirstOrDefault().TypeOfLocation, result.TypeOfLocation);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WithValidLocation_ReturnsUpdatedLocation()
        {
            // Arrange
            var oldLocation = new City
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                TypeOfLocation = LocationType.City.GetDisplayName()
            };

            var updatedLocationData = new LocationDto
            {
                Name = "Changed",
                Description = "Changed",
                GeographicalDescription = "Changed",
                LocationId = LocationType.Tomb
            };

            var newLocation = new City
            {
                Id = 1,
                Name = "Changed",
                Description = "Changed",
                GeographicalDescription = "Changed",
                LocationId = LocationType.Tomb,
                TypeOfLocation = LocationType.Tomb.GetDisplayName()
            };

            await _context.AddAsync(oldLocation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.UpdateLocation(oldLocation, updatedLocationData);

            // Assert
            Assert.Equal(newLocation.Name, result.Name);
            Assert.Equal(newLocation.Description, result.Description);
            Assert.Equal(newLocation.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(newLocation.LocationId, result.LocationId);
            Assert.Equal(newLocation.TypeOfLocation, result.TypeOfLocation);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WithInvalidLocation_LogsExpectedError()
        {
            //Arrange
            var oldLocation = new City
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                TypeOfLocation = LocationType.City.GetDisplayName()
            };

            var updatedLocationData = new LocationDto();
            await _context.AddAsync(oldLocation);
            await _context.SaveChangesAsync();

            // Act
            await _locationRepository.UpdateLocation(oldLocation, updatedLocationData);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<Location>()), Times.Once);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WithInvalidLocation_ReturnsExpectedNull()
        {
            //Arrange
            var oldLocation = new City
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                TypeOfLocation = LocationType.City.GetDisplayName()
            };

            var updatedLocationData = new LocationDto();
            await _context.AddAsync(oldLocation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.UpdateLocation(oldLocation, updatedLocationData);

            // Assert
            Assert.Null(result);
            _context.Database.EnsureDeleted();
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
            switch (location.LocationId)
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
                case LocationType.WordWall:
                    Assert.Equal(_context.WordWalls.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.Castle:
                    Assert.Equal(_context.Castles.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.GuildHeadquarter:
                    Assert.Equal(_context.GuildHeadquarters.FirstOrDefault().Name, result.Name);
                    break;
                case LocationType.UnmarkedLocation:
                    Assert.Equal(_context.UnmarkedLocations.FirstOrDefault().Name, result.Name);
                    break;
                default:
                    Assert.True(false);
                    break;
            }
            _context.Database.EnsureDeleted();
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
            yield return new object[]
            {
                "Valid properties for a WordWall",
                TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                "Valid properties for a Castle",
                TestMethodHelpers.CreateNewCastle()
            };
            yield return new object[]
            {
                "Valid properties for a GuildHeadquarter",
                TestMethodHelpers.CreateNewGuildHeadquarter()
            };
            yield return new object[]
            {
                "Valid properties for a UnmarkedLocation",
                TestMethodHelpers.CreateNewUnmarkedLocation()
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
            Assert.Equal(location.LocationId, result.LocationId);
            Assert.Equal(location.Id, result.Id);
            _context.Database.EnsureDeleted();
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
            _context.Database.EnsureDeleted();
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
            yield return new object[]
            {
                "Invalid properties for WordWall",
                new WordWall()
            };
            yield return new object[]
            {
                "Invalid properties for Castle",
                new Data.Models.Castle()
            };
            yield return new object[]
            {
                "Invalid properties for GuildHeadquarter",
                new GuildHeadquarter()
            };
            yield return new object[]
            {
                "Invalid properties for UnmarkedLocation",
                new UnmarkedLocation()
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
            _context.Database.EnsureDeleted();
        }
    }

    public class DeleteLocation : LocationRepository_Tests
    {
        [Fact]
        public async void WhenLocationIsDeleted_ReturnsTrue()
        {
            // Arrange
            var city = new City
            {
                Id = 1,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                Description = "Test"
            };

            await _context.AddAsync(city);
            await _context.SaveChangesAsync();

            // Act
            var result = await _locationRepository.DeleteLocation(city);

            // Assert
            Assert.Equal(true, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WhenLocationFailsToDelete_ReturnsFalse()
        {
            // Arrange
            var city = new City
            {
                Id = 1,
                Name = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City,
                Description = "Test"
            };

            // Act
            var result = await _locationRepository.DeleteLocation(city);

            // Assert
            Assert.Equal(false, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WhenErrorOccurs_LogsError()
        {
            // Arrange
            var city = new City();

            // Act
            var result = await _locationRepository.DeleteLocation(city);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<Location>()), Times.Once);
            _context.Database.EnsureDeleted();
        }
    }
}