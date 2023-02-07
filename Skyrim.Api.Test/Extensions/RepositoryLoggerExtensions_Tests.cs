using Microsoft.Extensions.Logging;
using Moq;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using RepositoryloggerExtensions = Skyrim.Api.Extensions.RepositoryLoggerExtensions;

namespace Skyrim.Api.Test.Extensions
{
    public class RepositoryLoggerExtensions_Tests
    {
        protected readonly RepositoryloggerExtensions _loggerExtensions;
        protected readonly Mock<ILogger<LocationRepository>> _partialMockLocationRepositoryLogger;
        protected Mock<IRepositoryLoggerExtension> _mockLoggerExtension;

        public RepositoryLoggerExtensions_Tests()
        {
            _partialMockLocationRepositoryLogger = new Mock<ILogger<LocationRepository>>();
            _mockLoggerExtension = new Mock<IRepositoryLoggerExtension>();
            _loggerExtensions = new RepositoryloggerExtensions(_partialMockLocationRepositoryLogger.Object);
        }
    }

    public class LogError : RepositoryLoggerExtensions_Tests
    {
        [Theory]
        [MemberData(nameof(FatalErrorsForEachLocationType))]
        public void WhenLoggerIsCalled_ErrorIsLoggedOnce(string description, Exception exception, Location location)
        {
            // Arrange

            // Act
            _mockLoggerExtension.Object.LogError(exception, location);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<Location>()), Times.Once());
        }
        public static IEnumerable<object[]> FatalErrorsForEachLocationType()
        {
            yield return new object[]
            {
                "Fatal Error for City location",
                new Exception(),
                new City
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.City
                }
            };
            yield return new object[]
            {
                "Fatal Error for Town location",
                new Exception(),
                new Town
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Town
                }
            };
            yield return new object[]
            {
                "Fatal Error for Homestead location",
                new Exception(),
                new Settlement
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Homestead
                }
            };
            yield return new object[]
            {
                "Fatal Error for Settlement location",
                new Exception(),
                new Homestead
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Settlement
                }
            };
            yield return new object[]
            {
                "Fatal Error for DaedricShrine location",
                new Exception(),
                new DaedricShrine
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DaedricShrine
                }
            };
            yield return new object[]
            {
                "Fatal Error for StandingStone location",
                new Exception(),
                new StandingStone
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.StandingStone
                }
            };
        }
    }
}
