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
    public void WhenLoggerIsCalled_ErrorIsLoggedOnce(string description, Exception exception, LocationDto createLocationDto)
    {
        // Arrange

        // Act
        _mockLoggerExtension.Object.LogError(exception, createLocationDto);

        // Assert
        _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<LocationDto>()), Times.Once());
    }
    public static IEnumerable<object[]> FatalErrorsForEachLocationType()
    {
        yield return new object[]
        {
                "Fatal Error for City location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                }
        };
        yield return new object[]
        {
                "Fatal Error for Town location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                }
        };
        yield return new object[]
        {
                "Fatal Error for Homestead location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Homestead
                }
        };
        yield return new object[]
        {
                "Fatal Error for Settlement location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                }
        };
        yield return new object[]
        {
                "Fatal Error for DaedricShrine location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DaedricShrine
                }
        };
        yield return new object[]
        {
                "Fatal Error for StandingStone location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.StandingStone
                }
        };
        yield return new object[]
        {
                "Fatal Error for Landmark location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Landmark
                }
        };
        yield return new object[]
        {
                "Fatal Error for Camp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Camp
                }
        };
        yield return new object[]
        {
                "Fatal Error for Cave location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Cave
                }
        };
        yield return new object[]
        {
                "Fatal Error for Clearing location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Clearing
                }
        };
        yield return new object[]
        {
                "Fatal Error for Dock location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Dock
                }
        };
        yield return new object[]
        {
                "Fatal Error for DragonLair location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DragonLair
                }
        };
        yield return new object[]
        {
                "Fatal Error for DwarvenRuin location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DwarvenRuin
                }
        };
        yield return new object[]
        {
                "Fatal Error for Farm location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Farm
                }
        };
        yield return new object[]
        {
                "Fatal Error for Fort location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Fort
                }
        };
        yield return new object[]
        {
                "Fatal Error for GiantCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.GiantCamp
                }
        };
        yield return new object[]
        {
                "Fatal Error for Grove location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Grove
                }
        };
        yield return new object[]
        {
                "Fatal Error for ImperialCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.ImperialCamp
                }
        };
        yield return new object[]
        {
                "Fatal Error for LightHouse location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.LightHouse
                }
        };
        yield return new object[]
        {
                "Fatal Error for Mine location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Mine
                }
        };
        yield return new object[]
        {
                "Fatal Error for NordicTower location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.NordicTower
                }
        };
        yield return new object[]
        {
                "Fatal Error for OrcStronghold location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.OrcStronghold
                }
        };
        yield return new object[]
        {
                "Fatal Error for Pass location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Pass
                }
        };
        yield return new object[]
        {
                "Fatal Error for Ruin location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Ruin
                }
        };
        yield return new object[]
        {
                "Fatal Error for Shack location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Shack
                }
        };
        yield return new object[]
        {
                "Fatal Error for Ship location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Ship
                }
        };
        yield return new object[]
        {
                "Fatal Error for Shipwreck location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Shipwreck
                }
        };
        yield return new object[]
        {
                "Fatal Error for Stable location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Stable
                }
        };
        yield return new object[]
        {
                "Fatal Error for StormcloakCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.StormcloakCamp
                }
        };
        yield return new object[]
        {
                "Fatal Error for Tomb location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Tomb
                }
        };
        yield return new object[]
        {
                "Fatal Error for Watchtower location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Watchtower
                }
        };
        yield return new object[]
        {
                "Fatal Error for WheatMill location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.WheatMill
                }
        };
        yield return new object[]
        {
                "Fatal Error for LumberMill location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.LumberMill
                }
        };
        yield return new object[]
        {
                "Fatal Error for BodyOfWater location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.BodyOfWater
                }
        };
        yield return new object[]
        {
                "Fatal Error for InnOrTavern location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.InnOrTavern
                }
        };
        yield return new object[]
        {
                "Fatal Error for Temple location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Temple
                }
        };
        yield return new object[]
        {
                "Fatal Error for WordWall location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.WordWall
                }
        };
        yield return new object[]
        {
                "Fatal Error for Castle location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Castle
                }
        };
        yield return new object[]
        {
                "Fatal Error for GuildHeadquarter location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.GuildHeadquarter
                }
        };
        yield return new object[]
        {
                "Fatal Error for UnmarkedLocation location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.UnmarkedLocation
                }
        };
    }

    [Theory]
    [MemberData(nameof(ErrorsAndLocationsForEachLocationType))]
    public void WhenLoggerIsCalled_LogsErrorForCorrectLocationType(string description, Exception exception, LocationDto createLocationDto,
        LocationType locationType)
    {
        // Arrange

        // Act
        _mockLoggerExtension.Object.LogError(exception, createLocationDto);

        // Assert
        Assert.Equal(locationType, createLocationDto.LocationId);
    }
    public static IEnumerable<object[]> ErrorsAndLocationsForEachLocationType()
    {
        yield return new object[]
        {
                "Fatal Error for City location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                },
                LocationType.City
        };
        yield return new object[]
        {
                "Fatal Error for Town location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                },
                LocationType.Town
        };
        yield return new object[]
        {
                "Fatal Error for Homestead location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Homestead
                },
                LocationType.Homestead
        };
        yield return new object[]
        {
                "Fatal Error for Settlement location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                },
                LocationType.Settlement
        };
        yield return new object[]
        {
                "Fatal Error for DaedricShrine location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DaedricShrine
                },
                LocationType.DaedricShrine
        };
        yield return new object[]
        {
                "Fatal Error for StandingStone location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.StandingStone
                },
                LocationType.StandingStone
        };
        yield return new object[]
        {
                "Fatal Error for Landmark location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Landmark
                },
                LocationType.Landmark
        };
        yield return new object[]
        {
                "Fatal Error for Camp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Camp
                },
                LocationType.Camp
        };
        yield return new object[]
        {
                "Fatal Error for Cave location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Cave
                },
                LocationType.Cave
        };
        yield return new object[]
        {
                "Fatal Error for Clearing location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Clearing
                },
                LocationType.Clearing
        };
        yield return new object[]
        {
                "Fatal Error for Dock location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Dock
                },
                LocationType.Dock
        };
        yield return new object[]
        {
                "Fatal Error for DragonLair location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DragonLair
                },
                LocationType.DragonLair
        };
        yield return new object[]
        {
                "Fatal Error for DwarvenRuin location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DwarvenRuin
                },
                LocationType.DwarvenRuin
        };
        yield return new object[]
        {
                "Fatal Error for Farm location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Farm
                },
                LocationType.Farm
        };
        yield return new object[]
        {
                "Fatal Error for Fort location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Fort
                },
                LocationType.Fort
        };
        yield return new object[]
        {
                "Fatal Error for GiantCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.GiantCamp
                },
                LocationType.GiantCamp
        };
        yield return new object[]
        {
                "Fatal Error for Grove location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Grove
                },
                LocationType.Grove
        };
        yield return new object[]
        {
                "Fatal Error for ImperialCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.ImperialCamp
                },
                LocationType.ImperialCamp
        };
        yield return new object[]
        {
                "Fatal Error for LightHouse location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.LightHouse
                },
                LocationType.LightHouse
        };
        yield return new object[]
        {
                "Fatal Error for Mine location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Mine
                },
                LocationType.Mine
        };
        yield return new object[]
        {
                "Fatal Error for NordicTower location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.NordicTower
                },
                LocationType.NordicTower
        };
        yield return new object[]
        {
                "Fatal Error for OrcStronghold location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.OrcStronghold
                },
                LocationType.OrcStronghold
        };
        yield return new object[]
        {
                "Fatal Error for Pass location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Pass
                },
                LocationType.Pass
        };
        yield return new object[]
        {
                "Fatal Error for Ruin location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Ruin
                },
                LocationType.Ruin
        };
        yield return new object[]
        {
                "Fatal Error for Shack location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Shack
                },
                LocationType.Shack
        };
        yield return new object[]
        {
                "Fatal Error for Ship location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Ship
                },
                LocationType.Ship
        };
        yield return new object[]
        {
                "Fatal Error for Shipwreck location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Shipwreck
                },
                LocationType.Shipwreck
        };
        yield return new object[]
        {
                "Fatal Error for Stable location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Stable
                },
                LocationType.Stable
        };
        yield return new object[]
        {
                "Fatal Error for StormcloakCamp location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.StormcloakCamp
                },
                LocationType.StormcloakCamp
        };
        yield return new object[]
        {
                "Fatal Error for Tomb location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Tomb
                },
                LocationType.Tomb
        };
        yield return new object[]
        {
                "Fatal Error for Watchtower location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Watchtower
                },
                LocationType.Watchtower
        };
        yield return new object[]
        {
                "Fatal Error for WheatMill location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.WheatMill
                },
                LocationType.WheatMill
        };
        yield return new object[]
        {
                "Fatal Error for LumberMill location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.LumberMill
                },
                LocationType.LumberMill
        };
        yield return new object[]
        {
                "Fatal Error for BodyOfWater location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.BodyOfWater
                },
                LocationType.BodyOfWater
        };
        yield return new object[]
        {
                "Fatal Error for InnOrTavern location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.InnOrTavern
                },
                LocationType.InnOrTavern
        };
        yield return new object[]
        {
                "Fatal Error for Temple location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Temple
                },
                LocationType.Temple
        };
        yield return new object[]
        {
                "Fatal Error for WordWall location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.WordWall
                },
                LocationType.WordWall
        };
        yield return new object[]
        {
                "Fatal Error for Castle location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Castle
                },
                LocationType.Castle
        };
        yield return new object[]
        {
                "Fatal Error for GuildHeadquarter location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.GuildHeadquarter
                },
                LocationType.GuildHeadquarter
        };
        yield return new object[]
        {
                "Fatal Error for UnmarkedLocation location",
                new Exception(),
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.UnmarkedLocation
                },
                LocationType.UnmarkedLocation
        };
    }
}
