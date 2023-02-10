﻿using Microsoft.Extensions.Logging;
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
        yield return new object[]
        {
                "Fatal Error for StandingStone location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.StandingStone
                }
        };
        yield return new object[]
        {
                "Fatal Error for Landmark location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Landmark
                }
        };
        yield return new object[]
        {
                "Fatal Error for Camp location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Camp
                }
        };
        yield return new object[]
        {
                "Fatal Error for Cave location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Cave
                }
        };
    }

    [Theory]
    [MemberData(nameof(ErrorsAndLocationsForEachLocationType))]
    public void WhenLoggerIsCalled_LogsErrorForCorrectLocationType(string description, Exception exception, CreateLocationDto createLocationDto,
        LocationType locationType)
    {
        // Arrange

        // Act
        _mockLoggerExtension.Object.LogError(exception, createLocationDto);

        // Assert
        Assert.Equal(locationType, createLocationDto.TypeOfLocation);
    }
    public static IEnumerable<object[]> ErrorsAndLocationsForEachLocationType()
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
                },
                LocationType.City
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
                },
                LocationType.Town
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
                },
                LocationType.Homestead
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
                },
                LocationType.Settlement
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
                },
                LocationType.DaedricShrine
        };
        yield return new object[]
        {
                "Fatal Error for StandingStone location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.StandingStone
                },
                LocationType.StandingStone
        };
        yield return new object[]
        {
                "Fatal Error for Landmark location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Landmark
                },
                LocationType.Landmark
        };
        yield return new object[]
        {
                "Fatal Error for Camp location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Camp
                },
                LocationType.Camp
        };
        yield return new object[]
        {
                "Fatal Error for Cave location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Cave
                },
                LocationType.Cave
        };
    }
}
