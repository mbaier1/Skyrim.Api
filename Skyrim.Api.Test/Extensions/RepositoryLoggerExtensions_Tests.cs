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
            yield return new object[]
            {
                "Fatal Error for Landmark location",
                new Exception(),
                new Landmark
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
                new Camp
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
                new Cave
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Cave
                }
            };
            yield return new object[]
            {
                "Fatal Error for Clearing location",
                new Exception(),
                new Clearing
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Clearing
                }
            };
            yield return new object[]
            {
                "Fatal Error for Dock location",
                new Exception(),
                new Dock
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Dock
                }
            };
            yield return new object[]
            {
                "Fatal Error for DragonLair location",
                new Exception(),
                new DragonLair
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DragonLair
                }
            };
            yield return new object[]
            {
                "Fatal Error for DwarvenRuin location",
                new Exception(),
                new DwarvenRuin
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DwarvenRuin
                }
            };
            yield return new object[]
            {
                "Fatal Error for Farm location",
                new Exception(),
                new Farm
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Farm
                }
            };
            yield return new object[]
            {
                "Fatal Error for Fort location",
                new Exception(),
                new Fort
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Fort
                }
            };
            yield return new object[]
            {
                "Fatal Error for GiantCamp location",
                new Exception(),
                new GiantCamp
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.GiantCamp
                }
            };
            yield return new object[]
            {
                "Fatal Error for Grove location",
                new Exception(),
                new Grove
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Grove
                }
            };
            yield return new object[]
            {
                "Fatal Error for ImperialCamp location",
                new Exception(),
                new ImperialCamp
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.ImperialCamp
                }
            };
            yield return new object[]
            {
                "Fatal Error for LightHouse location",
                new Exception(),
                new LightHouse
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.LightHouse
                }
            };
            yield return new object[]
            {
                "Fatal Error for Mine location",
                new Exception(),
                new Mine
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Mine
                }
            };
            yield return new object[]
            {
                "Fatal Error for NordicTower location",
                new Exception(),
                new NordicTower
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.NordicTower
                }
            };
            yield return new object[]
            {
                "Fatal Error for OrcStronghold location",
                new Exception(),
                new OrcStronghold
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.OrcStronghold
                }
            };
            yield return new object[]
            {
                "Fatal Error for Pass location",
                new Exception(),
                new Pass
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Pass
                }
            };
            yield return new object[]
            {
                "Fatal Error for Ruin location",
                new Exception(),
                new Ruin
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Ruin
                }
            };
        }

        [Theory]
        [MemberData(nameof(ErrorsAndLocationsForEachLocationType))]
        public void WhenLoggerIsCalled_LogsErrorForCorrectLocationType(string description, Exception exception, Location location,
            LocationType locationType)
        {
            // Arrange

            // Act
            _mockLoggerExtension.Object.LogError(exception, location);

            // Assert
            Assert.Equal(locationType, location.TypeOfLocation);
        }
        public static IEnumerable<object[]> ErrorsAndLocationsForEachLocationType()
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
                },
                LocationType.City
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
                },
                LocationType.Town
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
                },
                LocationType.Homestead
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
                },
                LocationType.Settlement
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
                ,
                LocationType.DaedricShrine
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
                },
                LocationType.StandingStone
            };
            yield return new object[]
            {
                "Fatal Error for Landmark location",
                new Exception(),
                new Landmark
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
                new Camp
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
                new Cave
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Cave
                },
                LocationType.Cave
            };
            yield return new object[]
            {
                "Fatal Error for Clearing location",
                new Exception(),
                new Clearing
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Clearing
                },
                LocationType.Clearing
            };
            yield return new object[]
            {
                "Fatal Error for Dock location",
                new Exception(),
                new Dock
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Dock
                },
                LocationType.Dock
            };
            yield return new object[]
            {
                "Fatal Error for DragonLair location",
                new Exception(),
                new DragonLair
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DragonLair
                },
                LocationType.DragonLair
            };
            yield return new object[]
            {
                "Fatal Error for DwarvenRuin location",
                new Exception(),
                new DwarvenRuin
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DwarvenRuin
                },
                LocationType.DwarvenRuin
            };
            yield return new object[]
            {
                "Fatal Error for Farm location",
                new Exception(),
                new Farm
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Farm
                },
                LocationType.Farm
            };
            yield return new object[]
            {
                "Fatal Error for Fort location",
                new Exception(),
                new Fort
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Fort
                },
                LocationType.Fort
            };
            yield return new object[]
            {
                "Fatal Error for GiantCamp location",
                new Exception(),
                new GiantCamp
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.GiantCamp
                },
                LocationType.GiantCamp
            };
            yield return new object[]
            {
                "Fatal Error for Grove location",
                new Exception(),
                new Grove
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Grove
                },
                LocationType.Grove
            };
            yield return new object[]
            {
                "Fatal Error for ImperialCamp location",
                new Exception(),
                new ImperialCamp
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.ImperialCamp
                },
                LocationType.ImperialCamp
            };
            yield return new object[]
            {
                "Fatal Error for LightHouse location",
                new Exception(),
                new LightHouse
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.LightHouse
                },
                LocationType.LightHouse
            };
            yield return new object[]
            {
                "Fatal Error for Mine location",
                new Exception(),
                new Mine
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Mine
                },
                LocationType.Mine
            };
            yield return new object[]
            {
                "Fatal Error for NordicTower location",
                new Exception(),
                new NordicTower
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.NordicTower
                },
                LocationType.NordicTower
            };
            yield return new object[]
            {
                "Fatal Error for OrcStronghold location",
                new Exception(),
                new OrcStronghold
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.OrcStronghold
                },
                LocationType.OrcStronghold
            };
            yield return new object[]
            {
                "Fatal Error for Pass location",
                new Exception(),
                new Pass
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Pass
                },
                LocationType.Pass
            };
            yield return new object[]
            {
                "Fatal Error for Ruin location",
                new Exception(),
                new Ruin
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Ruin
                },
                LocationType.Ruin
            };
        }
    }
}
