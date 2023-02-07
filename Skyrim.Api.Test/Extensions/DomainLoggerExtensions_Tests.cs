using Microsoft.Extensions.Logging;
using Moq;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Test.Extensions;

namespace Skyrim.Api.Test.Extensions
{
    public class DomainLoggerExtensions_Tests
    {
        protected readonly DomainLoggerExtensions _loggerExtensions;
        protected readonly Mock<ILogger<LocationDomain>> _partialMockDomainRepositoryLogger;
        protected Mock<IDomainLoggerExtension> _mockLoggerExtension;
        public DomainLoggerExtensions_Tests()
        {
            _partialMockDomainRepositoryLogger = new Mock<ILogger<LocationDomain>>();
            _mockLoggerExtension = new Mock<IDomainLoggerExtension>();
            _loggerExtensions = new DomainLoggerExtensions(_partialMockDomainRepositoryLogger.Object);
        }
    }
}

public class LogError : DomainLoggerExtensions_Tests
{
    [Theory]
    [MemberData(nameof(FatalErrorsForEachLocationType))]
    public void WhenLoggerIsCalled_ErrorIsLoggedOnce(string description, Exception exception, CreateLocationDto createLocationDto)
    {
        // Arrange

        // Act
        _mockLoggerExtension.Object.LogError(exception, createLocationDto);

        // Assert
        _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once());
    }
    public static IEnumerable<object[]> FatalErrorsForEachLocationType()
    {
        yield return new object[]
        {
                "Fatal Error for City location",
                new Exception(),
                new CreateLocationDto
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
                new CreateLocationDto
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
                new CreateLocationDto
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
                new CreateLocationDto
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
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DaedricShrine
                }
        };
    }
}
