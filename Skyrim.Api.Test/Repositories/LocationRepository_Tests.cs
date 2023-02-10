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