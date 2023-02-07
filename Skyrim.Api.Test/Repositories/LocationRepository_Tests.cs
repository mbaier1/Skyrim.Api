using Microsoft.EntityFrameworkCore;
using Moq;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using Skyrim.Api.Repository.Interface;

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

        protected static City CreateNewCity()
        {
            return new City
            {
                Name = "Test City Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.City
            };
        }

        protected static Town CreateNewTown()
        {
            return new Town
            {
                Name = "Test City Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.Town
            };
        }

        protected static Homestead CreateNewHomestead()
        {
            return new Homestead
            {
                Name = "Test Homestead Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.Homestead
            };
        }

        protected static Settlement CreateNewSettlement()
        {
            return new Settlement
            {
                Name = "Test Settlement Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.Settlement
            };
        }

        protected static DaedricShrine CreateNewDaedricShrine()
        {
            return new DaedricShrine
            {
                Name = "Test DaedricShrine Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.DaedricShrine
            };
        }

        protected static StandingStone CreateNewStandingStone()
        {
            return new StandingStone
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test"
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsCity()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsTown()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Town
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsHomestead()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Homestead
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsSettlement()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Settlement
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsDaedricShrine()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.DaedricShrine
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsStandingStone()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.StandingStone
            };
        }
    }

    public class SaveLocation : LocationRepository_Tests
    {
        [Theory]
        [MemberData(nameof(ValidLocationForEachLocationType))]
        public async void WithValidCreateLocationDto_SavesExpectedLocation(Location location)
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
            }
        }
        public static IEnumerable<object[]> ValidLocationForEachLocationType()
        {
            yield return new object[]
            {
                CreateNewCity()
            };
            yield return new object[]
            {
                CreateNewTown()
            };
            yield return new object[]
            {
                CreateNewHomestead()
            };
            yield return new object[]
            {
                CreateNewSettlement()
            };
            yield return new object[]
            {
                CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                CreateNewStandingStone()
            };
        }

        [Theory]
        [MemberData(nameof(ValidLocationForEachLocationType))]
        public async void WithValidLocations_ReturnsExpectedLocation(Location location)
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