using AutoMapper;
using Microsoft.CodeAnalysis;
using Moq;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Test.TestHelpers;
using Location = Skyrim.Api.Data.AbstractModels.Location;

namespace Skyrim.Api.Test.Domains
{
    public class LocationDomain_Tests
    {
        protected readonly ILocationDomain _locationDomain;
        protected readonly Mock<ILocationRepository> _mockLocationRepository;
        protected readonly Mock<ILocationDtoFormatHelper> _mockCreateLocationDtoFormatHelper;
        protected Mock<IDomainLoggerExtension> _mockLoggerExtension;
        protected Mock<IMapper> _mockMapper;

        public LocationDomain_Tests()
        {
            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockCreateLocationDtoFormatHelper = new Mock<ILocationDtoFormatHelper>();
            _mockLoggerExtension = new Mock<IDomainLoggerExtension>();
            _mockMapper = new Mock<IMapper>();
            _locationDomain = new LocationDomain(_mockLocationRepository.Object, _mockCreateLocationDtoFormatHelper.Object,
                _mockLoggerExtension.Object, _mockMapper.Object);
        }
    }

    public class GetLocations : LocationDomain_Tests
    {
        [Fact]
        public async void WithDataInDatabase_ReturnsExpectedLocations()
        {
            // Arrange
            var locations = new List<Location>
            {
                new City
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                },
                new Town
                {
                    Id = 2,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                },
                new Settlement
                {
                    Id = 3,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                }
            };
            _mockLocationRepository.Setup(x => x.GetLocation()).ReturnsAsync(locations);

            // Act
            var result = await _locationDomain.GetLocation();

            // Assert
            Assert.Equal(locations[0].Id, result.ToList()[0].Id);
            Assert.Equal(locations[1].Id, result.ToList()[1].Id);
            Assert.Equal(locations[2].Id, result.ToList()[2].Id);
        }

        [Fact]
        public async void WithDataInDatabase_HasCorrectCountOfLocations()
        {
            // Arrange
            List<Location> locations = new List<Location>
            {
                new City
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                },
                new Town
                {
                    Id = 2,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                },
                new Settlement
                {
                    Id = 3,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                }
            };
            _mockLocationRepository.Setup(x => x.GetLocation()).ReturnsAsync(locations);

            // Act
            var result = await _locationDomain.GetLocation();

            // Assert
            Assert.Equal(locations.Count(), result.ToList().Count());
        }

        [Fact]
        public async void WithNoDataInDatabaseOrErrorOccurs_ReturnsNull()
        {
            // Arrange
            var locations = new List<Location>();
            _mockLocationRepository.Setup(x => x.GetLocation()).ReturnsAsync(locations);

            // Act
            var result = await _locationDomain.GetLocation();

            // Assert
            Assert.Null(result);
        }
    }

    public class GetLocation : LocationDomain_Tests
    {
        [Fact]
        public async void WhenIdIsValid_ReturnsExpectedLocation()
        {
            // Arrange
            int id = 0;
            var city = TestMethodHelpers.CreateNewCity();
            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ReturnsAsync(city);

            // Act

            var result = await _locationDomain.GetLocation(id);

            // Assert
            Assert.Equal(id, city.Id);
        }

        [Fact]
        public async void WhenIdIsInvalid_ReturnsExpectedNull()
        {
            // Arrange
            int id = 1;

            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ReturnsAsync((City)null);

            // Act
            var result = await _locationDomain.GetLocation(id);

            // Assert
            Assert.Null(result);
        }
    }

    public class CreateLocation : LocationDomain_Tests
    {
        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public async void WhenCreateLocationDtoHasValidProperties_ReturnsExpectedLocation(string description, LocationDto createLocationDto,
           Location taskType, Location type)
        {
            // Arrange
            if (type.LocationId == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (type.LocationId == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (type.LocationId == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (type.LocationId == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (type.LocationId == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (type.LocationId == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (type.LocationId == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (type.LocationId == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (type.LocationId == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (type.LocationId == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (type.LocationId == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (type.LocationId == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (type.LocationId == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (type.LocationId == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());
            else if (type.LocationId == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (type.LocationId == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (type.LocationId == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (type.LocationId == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (type.LocationId == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (type.LocationId == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (type.LocationId == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (type.LocationId == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (type.LocationId == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (type.LocationId == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (type.LocationId == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (type.LocationId == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (type.LocationId == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (type.LocationId == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (type.LocationId == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (type.LocationId == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (type.LocationId == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (type.LocationId == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (type.LocationId == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (type.LocationId == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (type.LocationId == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (type.LocationId == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (type.LocationId == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());
            else if (type.LocationId == LocationType.Castle)
                _mockMapper.Setup(x => x.Map<Data.Models.Castle>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCastle());
            else if (type.LocationId == LocationType.GuildHeadquarter)
                _mockMapper.Setup(x => x.Map<GuildHeadquarter>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGuildHeadquarter());
            else if (type.LocationId == LocationType.UnmarkedLocation)
                _mockMapper.Setup(x => x.Map<UnmarkedLocation>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewUnmarkedLocation());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(type.Id, result.Id);
            Assert.Equal(type.Name, result.Name);
            Assert.Equal(type.Description, result.Description);
            Assert.Equal(type.LocationId, result.LocationId);
            Assert.Equal(type.GeographicalDescription, result.GeographicalDescription);
        }
        public static IEnumerable<object[]> ValidPropertiesForEachLocationType()
        {
            yield return new object[]
            {
                "Valid properties for City Location",
                TestMethodHelpers.CreateNewLocationDtoAsCity(),
                new City
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test"
                },
                new City
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Town Location",
                TestMethodHelpers.CreateNewLocationDtoAsTown(),
                new Town
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test"
                },
                new Town
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Homestead Location",
                TestMethodHelpers.CreateNewLocationDtoAsHomestead(),
                new Homestead
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test"
                },
                new Homestead
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Settlement Location",
                TestMethodHelpers.CreateNewLocationDtoAsSettlement(),
                new Settlement
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test"
                },
                new Settlement
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for DaedricShrine Location",
                TestMethodHelpers.CreateNewLocationDtoAsDaedricShrine(),
                new DaedricShrine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test"
                },
                new DaedricShrine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for StandingStone Location",
                TestMethodHelpers.CreateNewLocationDtoAsStandingStone(),
                new StandingStone
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test"
                },
                new StandingStone
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Landmark Location",
                TestMethodHelpers.CreateNewLocationDtoAsLandmark(),
                new Landmark
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test"
                },
                new Landmark
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Camp Location",
                TestMethodHelpers.CreateNewLocationDtoAsCamp(),
                new Camp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test"
                },
                new Camp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Cave Location",
                TestMethodHelpers.CreateNewLocationDtoAsCave(),
                new Cave
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test"
                },
                new Cave
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Clearing Location",
                TestMethodHelpers.CreateNewLocationDtoAsClearing(),
                new Clearing
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test"
                },
                new Clearing
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Dock Location",
                TestMethodHelpers.CreateNewLocationDtoAsDock(),
                new Dock
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test"
                },
                new Dock
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for DragonLair Location",
                TestMethodHelpers.CreateNewLocationDtoAsDragonLair(),
                new DragonLair
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test"
                },
                new DragonLair
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for DwarvenRuin Location",
                TestMethodHelpers.CreateNewLocationDtoAsDwarvenRuin(),
                new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test"
                },
                new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Farm Location",
                TestMethodHelpers.CreateNewLocationDtoAsFarm(),
                new Farm
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test"
                },
                new Farm
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Fort Location",
                TestMethodHelpers.CreateNewLocationDtoAsFort(),
                new Fort
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test"
                },
                new Fort
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for GiantCamp Location",
                TestMethodHelpers.CreateNewLocationDtoAsGiantCamp(),
                new GiantCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test"
                },
                new GiantCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Grove Location",
                TestMethodHelpers.CreateNewLocationDtoAsGrove(),
                new Grove
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test"
                },
                new Grove
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for ImperialCamp Location",
                TestMethodHelpers.CreateNewLocationDtoAsImperialCamp(),
                new ImperialCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test"
                },
                new ImperialCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for LightHouse Location",
                TestMethodHelpers.CreateNewLocationDtoAsLightHouse(),
                new LightHouse
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test"
                },
                new LightHouse
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Mine Location",
                TestMethodHelpers.CreateNewLocationDtoAsMine(),
                new Mine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test"
                },
                new Mine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for NordicTower Location",
                TestMethodHelpers.CreateNewLocationDtoAsNordicTower(),
                new NordicTower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test"
                },
                new NordicTower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for OrcStronghold Location",
                TestMethodHelpers.CreateNewLocationDtoAsOrcStronghold(),
                new OrcStronghold
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test"
                },
                new OrcStronghold
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Pass Location",
                TestMethodHelpers.CreateNewLocationDtoAsPass(),
                new Pass
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test"
                },
                new Pass
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Ruin Location",
                TestMethodHelpers.CreateNewLocationDtoAsRuin(),
                new Ruin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test"
                },
                new Ruin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Shack Location",
                TestMethodHelpers.CreateNewLocationDtoAsShack(),
                new Shack
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test"
                },
                new Shack
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Ship Location",
                TestMethodHelpers.CreateNewLocationDtoAsShip(),
                new Ship
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test"
                },
                new Ship
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Shipwreck Location",
                TestMethodHelpers.CreateNewLocationDtoAsShipwreck(),
                new Shipwreck
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test"
                },
                new Shipwreck
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Stable Location",
                TestMethodHelpers.CreateNewLocationDtoAsStable(),
                new Stable
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test"
                },
                new Stable
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for StormcloakCamp Location",
                TestMethodHelpers.CreateNewLocationDtoAsStormcloakCamp(),
                new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test"
                },
                new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Tomb Location",
                TestMethodHelpers.CreateNewLocationDtoAsTomb(),
                new Tomb
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test"
                },
                new Tomb
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Watchtower Location",
                TestMethodHelpers.CreateNewLocationDtoAsWatchtower(),
                new Watchtower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test"
                },
                new Watchtower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for WheatMill Location",
                TestMethodHelpers.CreateNewLocationDtoAsWheatMill(),
                new WheatMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test"
                },
                new WheatMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for LumberMill Location",
                TestMethodHelpers.CreateNewLocationDtoAsLumberMill(),
                new LumberMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test"
                },
                new LumberMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for BodyOfWater Location",
                TestMethodHelpers.CreateNewLocationDtoAsBodyOfWater(),
                new BodyOfWater
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test"
                },
                new BodyOfWater
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for InnOrTavern Location",
                TestMethodHelpers.CreateNewLocationDtoAsInnOrTavern(),
                new InnOrTavern
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test"
                },
                new InnOrTavern
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Temple Location",
                TestMethodHelpers.CreateNewLocationDtoAsTemple(),
                new Temple
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test"
                },
                new Temple
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for WordWall Location",
                TestMethodHelpers.CreateNewLocationDtoAsWordWall(),
                new WordWall
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test"
                },
                new WordWall
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Castle Location",
                TestMethodHelpers.CreateNewLocationDtoAsCastle(),
                new Data.Models.Castle
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test"
                },
                new Data.Models.Castle
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for GuildHeadquarter Location",
                TestMethodHelpers.CreateNewLocationDtoAsGuildHeadquarter(),
                new GuildHeadquarter
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test"
                },
                new GuildHeadquarter
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for UnmarkedLocation Location",
                TestMethodHelpers.CreateNewLocationDtoAsUnmarkedLocation(),
                new UnmarkedLocation
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test"
                },
                new UnmarkedLocation
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test"
                }
            };
        }

        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public async void WithValidProperties_MapsToCorrectLocation(string description, LocationDto createLocationDto, Location taskType,
            Location location)
        {
            // Arrange
            if (location.LocationId == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (location.LocationId == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (location.LocationId == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (location.LocationId == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (location.LocationId == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (location.LocationId == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (location.LocationId == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (location.LocationId == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (location.LocationId == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (location.LocationId == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (location.LocationId == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (location.LocationId == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (location.LocationId == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (location.LocationId == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());
            else if (location.LocationId == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (location.LocationId == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (location.LocationId == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (location.LocationId == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (location.LocationId == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (location.LocationId == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (location.LocationId == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (location.LocationId == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (location.LocationId == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (location.LocationId == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (location.LocationId == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (location.LocationId == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (location.LocationId == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (location.LocationId == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (location.LocationId == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (location.LocationId == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (location.LocationId == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (location.LocationId == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (location.LocationId == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (location.LocationId == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (location.LocationId == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (location.LocationId == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (location.LocationId == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());
            else if (location.LocationId == LocationType.Castle)
                _mockMapper.Setup(x => x.Map<Data.Models.Castle>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCastle());
            else if (location.LocationId == LocationType.GuildHeadquarter)
                _mockMapper.Setup(x => x.Map<GuildHeadquarter>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGuildHeadquarter());
            else if (location.LocationId == LocationType.UnmarkedLocation)
                _mockMapper.Setup(x => x.Map<UnmarkedLocation>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewUnmarkedLocation());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);

            // Act
            await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            if (location.LocationId == LocationType.City)
                _mockMapper.Verify(x => x.Map<City>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Town)
                _mockMapper.Verify(x => x.Map<Town>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Homestead)
                _mockMapper.Verify(x => x.Map<Homestead>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Settlement)
                _mockMapper.Verify(x => x.Map<Settlement>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.DaedricShrine)
                _mockMapper.Verify(x => x.Map<DaedricShrine>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.StandingStone)
                _mockMapper.Verify(x => x.Map<StandingStone>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Landmark)
                _mockMapper.Verify(x => x.Map<Landmark>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Camp)
                _mockMapper.Verify(x => x.Map<Camp>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Cave)
                _mockMapper.Verify(x => x.Map<Cave>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Clearing)
                _mockMapper.Verify(x => x.Map<Clearing>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Dock)
                _mockMapper.Verify(x => x.Map<Dock>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.DragonLair)
                _mockMapper.Verify(x => x.Map<DragonLair>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.DwarvenRuin)
                _mockMapper.Verify(x => x.Map<DwarvenRuin>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Farm)
                _mockMapper.Verify(x => x.Map<Farm>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Fort)
                _mockMapper.Verify(x => x.Map<Fort>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.GiantCamp)
                _mockMapper.Verify(x => x.Map<GiantCamp>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Grove)
                _mockMapper.Verify(x => x.Map<Grove>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.ImperialCamp)
                _mockMapper.Verify(x => x.Map<ImperialCamp>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.LightHouse)
                _mockMapper.Verify(x => x.Map<LightHouse>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Mine)
                _mockMapper.Verify(x => x.Map<Mine>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.NordicTower)
                _mockMapper.Verify(x => x.Map<NordicTower>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.OrcStronghold)
                _mockMapper.Verify(x => x.Map<OrcStronghold>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Pass)
                _mockMapper.Verify(x => x.Map<Pass>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Ruin)
                _mockMapper.Verify(x => x.Map<Ruin>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Shack)
                _mockMapper.Verify(x => x.Map<Shack>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Ship)
                _mockMapper.Verify(x => x.Map<Ship>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Shipwreck)
                _mockMapper.Verify(x => x.Map<Shipwreck>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Stable)
                _mockMapper.Verify(x => x.Map<Stable>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.StormcloakCamp)
                _mockMapper.Verify(x => x.Map<StormcloakCamp>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Tomb)
                _mockMapper.Verify(x => x.Map<Tomb>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Watchtower)
                _mockMapper.Verify(x => x.Map<Watchtower>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.WheatMill)
                _mockMapper.Verify(x => x.Map<WheatMill>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.LumberMill)
                _mockMapper.Verify(x => x.Map<LumberMill>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.BodyOfWater)
                _mockMapper.Verify(x => x.Map<BodyOfWater>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.InnOrTavern)
                _mockMapper.Verify(x => x.Map<InnOrTavern>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Temple)
                _mockMapper.Verify(x => x.Map<Temple>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.WordWall)
                _mockMapper.Verify(x => x.Map<WordWall>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.Castle)
                _mockMapper.Verify(x => x.Map<Data.Models.Castle>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.GuildHeadquarter)
                _mockMapper.Verify(x => x.Map<GuildHeadquarter>(createLocationDto), Times.Once());
            else if (location.LocationId == LocationType.UnmarkedLocation)
                _mockMapper.Verify(x => x.Map<UnmarkedLocation>(createLocationDto), Times.Once());
            else
                Assert.True(false);
        }

        [Theory]
        [MemberData(nameof(InvalidProperties))]
        public async void WithInvalidProperties_MappingFails_AndReturnsNull(string description, LocationDto createLocationDto)
        {
            // Arrange
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);

            // Act
            var result = _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(null, result.Result);
        }
        public static IEnumerable<object[]> InvalidProperties()
        {
            yield return new object[]
            {
                "Invalid properties for City",
                new LocationDto { LocationId = LocationType.City }
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                new LocationDto { LocationId = LocationType.Town }
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                new LocationDto { LocationId = LocationType.Homestead }
            };
            yield return new object[]
            {
                "Invalid properties for Settlement",
                new LocationDto { LocationId = LocationType.Settlement }
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                new LocationDto { LocationId = LocationType.DaedricShrine }
            };
            yield return new object[]
            {
                "Invalid properties for StandingStone",
                new LocationDto { LocationId = LocationType.StandingStone }
            };
            yield return new object[]
            {
                "Invalid properties for Landmark",
                new LocationDto { LocationId = LocationType.Landmark }
            };
            yield return new object[]
            {
                "Invalid properties for Camp",
                new LocationDto { LocationId = LocationType.Camp }
            };
            yield return new object[]
            {
                "Invalid properties for Cave",
                new LocationDto { LocationId = LocationType.Cave }
            };
            yield return new object[]
            {
                "Invalid properties for Clearing",
                new LocationDto { LocationId = LocationType.Clearing }
            };
            yield return new object[]
            {
                "Invalid properties for Dock",
                new LocationDto { LocationId = LocationType.Dock }
            };
            yield return new object[]
            {
                "Invalid properties for DragonLair",
                new LocationDto { LocationId = LocationType.DragonLair }
            };
            yield return new object[]
            {
                "Invalid properties for DwarvenRuin",
                new LocationDto { LocationId = LocationType.DwarvenRuin }
            };
            yield return new object[]
            {
                "Invalid properties for Farm",
                new LocationDto { LocationId = LocationType.Farm }
            };
            yield return new object[]
            {
                "Invalid properties for Fort",
                new LocationDto { LocationId = LocationType.Fort }
            };
            yield return new object[]
            {
                "Invalid properties for GiantCamp",
                new LocationDto { LocationId = LocationType.GiantCamp }
            };
            yield return new object[]
            {
                "Invalid properties for Grove",
                new LocationDto { LocationId = LocationType.Grove }
            };
            yield return new object[]
            {
                "Invalid properties for ImperialCamp",
                new LocationDto { LocationId = LocationType.ImperialCamp }
            };
            yield return new object[]
            {
                "Invalid properties for LightHouse",
                new LocationDto { LocationId = LocationType.LightHouse }
            };
            yield return new object[]
            {
                "Invalid properties for Mine",
                new LocationDto { LocationId = LocationType.Mine }
            };
            yield return new object[]
            {
                "Invalid properties for NordicTower",
                new LocationDto { LocationId = LocationType.NordicTower }
            };
            yield return new object[]
            {
                "Invalid properties for OrcStronghold",
                new LocationDto { LocationId = LocationType.OrcStronghold }
            };
            yield return new object[]
            {
                "Invalid properties for Pass",
                new LocationDto { LocationId = LocationType.Pass }
            };
            yield return new object[]
            {
                "Invalid properties for Ruin",
                new LocationDto { LocationId = LocationType.Ruin }
            };
            yield return new object[]
            {
                "Invalid properties for Shack",
                new LocationDto { LocationId = LocationType.Shack }
            };
            yield return new object[]
            {
                "Invalid properties for Ship",
                new LocationDto { LocationId = LocationType.Ship }
            };
            yield return new object[]
            {
                "Invalid properties for Shipwreck",
                new LocationDto { LocationId = LocationType.Shipwreck }
            };
            yield return new object[]
            {
                "Invalid properties for Stable",
                new LocationDto { LocationId = LocationType.Stable }
            };
            yield return new object[]
            {
                "Invalid properties for StormcloakCamp",
                new LocationDto { LocationId = LocationType.StormcloakCamp }
            };
            yield return new object[]
            {
                "Invalid properties for Tomb",
                new LocationDto { LocationId = LocationType.Tomb }
            };
            yield return new object[]
            {
                "Invalid properties for Watchtower",
                new LocationDto { LocationId = LocationType.Watchtower }
            };
            yield return new object[]
            {
                "Invalid properties for WheatMill",
                new LocationDto { LocationId = LocationType.WheatMill }
            };
            yield return new object[]
            {
                "Invalid properties for LumberMill",
                new LocationDto { LocationId = LocationType.LumberMill }
            };
            yield return new object[]
            {
                "Invalid properties for BodyOfWater",
                new LocationDto { LocationId = LocationType.BodyOfWater }
            };
            yield return new object[]
            {
                "Invalid properties for InnOrTavern",
                new LocationDto { LocationId = LocationType.InnOrTavern }
            };
            yield return new object[]
            {
                "Invalid properties for Temple",
                new LocationDto { LocationId = LocationType.Temple }
            };
            yield return new object[]
            {
                "Invalid properties for WordWall",
                new LocationDto { LocationId = LocationType.WordWall }
            };
            yield return new object[]
            {
                "Invalid properties for Castle",
                new LocationDto { LocationId = LocationType.Castle }
            };
            yield return new object[]
           {
                "Invalid properties for GuildHeadquarter",
                new LocationDto { LocationId = LocationType.GuildHeadquarter }
           };
            yield return new object[]
           {
                "Invalid properties for UnmarkedLocation",
                new LocationDto { LocationId = LocationType.UnmarkedLocation }
           };
        }

        [Theory]
        [MemberData(nameof(InvalidProperteriesCausesLoggingError))]
        public async void WithInvalidProperties_LogsExpectedError(string description, Location location, LocationDto createLocationDto)
        {
            // Arrange
            if (location.LocationId == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.Castle)
                _mockMapper.Setup(x => x.Map<Data.Models.Castle>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.GuildHeadquarter)
                _mockMapper.Setup(x => x.Map<GuildHeadquarter>(It.IsAny<LocationDto>())).Throws(new Exception());
            else if (location.LocationId == LocationType.UnmarkedLocation)
                _mockMapper.Setup(x => x.Map<UnmarkedLocation>(It.IsAny<LocationDto>())).Throws(new Exception());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);

            // Act
            await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<LocationDto>()), Times.Once);

        }
        public static IEnumerable<object[]> InvalidProperteriesCausesLoggingError()
        {
            yield return new object[]
            {
                "Invalid properties for City",
                TestMethodHelpers.CreateNewCity(),
                TestMethodHelpers.CreateNewLocationDtoAsCity(),
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                TestMethodHelpers.CreateNewTown(),
                TestMethodHelpers.CreateNewLocationDtoAsTown()
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                TestMethodHelpers.CreateNewHomestead(),
                TestMethodHelpers.CreateNewLocationDtoAsHomestead()
            };
            yield return new object[]
            {
                "Invalid properties for Settlement",
                TestMethodHelpers.CreateNewSettlement(),
                TestMethodHelpers.CreateNewLocationDtoAsSettlement()
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                TestMethodHelpers.CreateNewDaedricShrine(),
                TestMethodHelpers.CreateNewLocationDtoAsDaedricShrine()
            };
            yield return new object[]
            {
                "Invalid properties for StandingStone",
                TestMethodHelpers.CreateNewStandingStone(),
                TestMethodHelpers.CreateNewLocationDtoAsStandingStone()
            };
            yield return new object[]
            {
                "Invalid properties for Landmark",
                TestMethodHelpers.CreateNewLandmark(),
                TestMethodHelpers.CreateNewLocationDtoAsLandmark()
            };
            yield return new object[]
            {
                "Invalid properties for Camp",
                TestMethodHelpers.CreateNewCamp(),
                TestMethodHelpers.CreateNewLocationDtoAsCamp()
            };
            yield return new object[]
            {
                "Invalid properties for Cave",
                TestMethodHelpers.CreateNewCave(),
                TestMethodHelpers.CreateNewLocationDtoAsCave()
            };
            yield return new object[]
            {
                "Invalid properties for Clearing",
                TestMethodHelpers.CreateNewClearing(),
                TestMethodHelpers.CreateNewLocationDtoAsClearing()
            };
            yield return new object[]
            {
                "Invalid properties for Dock",
                TestMethodHelpers.CreateNewDock(),
                TestMethodHelpers.CreateNewLocationDtoAsDock()
            };
            yield return new object[]
            {
                "Invalid properties for DragonLair",
                TestMethodHelpers.CreateNewDragonLair(),
                TestMethodHelpers.CreateNewLocationDtoAsDragonLair()
            };
            yield return new object[]
            {
                "Invalid properties for DwarvenRuin",
                TestMethodHelpers.CreateNewDwarvenRuin(),
                TestMethodHelpers.CreateNewLocationDtoAsDwarvenRuin()
            };
            yield return new object[]
            {
                "Invalid properties for Farm",
                TestMethodHelpers.CreateNewFarm(),
                TestMethodHelpers.CreateNewLocationDtoAsFarm()
            };
            yield return new object[]
            {
                "Invalid properties for Fort",
                TestMethodHelpers.CreateNewFort(),
                TestMethodHelpers.CreateNewLocationDtoAsFort()
            };
            yield return new object[]
           {
                "Invalid properties for GiantCamp",
                TestMethodHelpers.CreateNewGiantCamp(),
                TestMethodHelpers.CreateNewLocationDtoAsGiantCamp()
           };
            yield return new object[]
           {
                "Invalid properties for Grove",
                TestMethodHelpers.CreateNewGrove(),
                TestMethodHelpers.CreateNewLocationDtoAsGrove()
           };
            yield return new object[]
           {
                "Invalid properties for ImperialCamp",
                TestMethodHelpers.CreateNewImperialCamp(),
                TestMethodHelpers.CreateNewLocationDtoAsImperialCamp()
           };
            yield return new object[]
           {
                "Invalid properties for LightHouse",
                TestMethodHelpers.CreateNewLightHouse(),
                TestMethodHelpers.CreateNewLocationDtoAsLightHouse()
           };
            yield return new object[]
           {
                "Invalid properties for Mine",
                TestMethodHelpers.CreateNewMine(),
                TestMethodHelpers.CreateNewLocationDtoAsMine()
           };
            yield return new object[]
           {
                "Invalid properties for NordicTower",
                TestMethodHelpers.CreateNewNordicTower(),
                TestMethodHelpers.CreateNewLocationDtoAsNordicTower()
           };
            yield return new object[]
           {
                "Invalid properties for OrcStronghold",
                TestMethodHelpers.CreateNewOrcStronghold(),
                TestMethodHelpers.CreateNewLocationDtoAsOrcStronghold()
           };
            yield return new object[]
           {
                "Invalid properties for Pass",
                TestMethodHelpers.CreateNewPass(),
                TestMethodHelpers.CreateNewLocationDtoAsPass()
           };
            yield return new object[]
           {
                "Invalid properties for Ruin",
                TestMethodHelpers.CreateNewRuin(),
                TestMethodHelpers.CreateNewLocationDtoAsRuin()
           };
            yield return new object[]
           {
                "Invalid properties for Shack",
                TestMethodHelpers.CreateNewShack(),
                TestMethodHelpers.CreateNewLocationDtoAsShack()
           };
            yield return new object[]
           {
                "Invalid properties for Ship",
                TestMethodHelpers.CreateNewShip(),
                TestMethodHelpers.CreateNewLocationDtoAsShip()
           };
            yield return new object[]
           {
                "Invalid properties for Shipwreck",
                TestMethodHelpers.CreateNewShipwreck(),
                TestMethodHelpers.CreateNewLocationDtoAsShipwreck()
           };
            yield return new object[]
           {
                "Invalid properties for Stable",
                TestMethodHelpers.CreateNewStable(),
                TestMethodHelpers.CreateNewLocationDtoAsStable()
           };
            yield return new object[]
           {
                "Invalid properties for StormcloakCamp",
                TestMethodHelpers.CreateNewStormcloakCamp(),
                TestMethodHelpers.CreateNewLocationDtoAsStormcloakCamp()
           };
            yield return new object[]
           {
                "Invalid properties for Tomb",
                TestMethodHelpers.CreateNewTomb(),
                TestMethodHelpers.CreateNewLocationDtoAsTomb()
           };
            yield return new object[]
           {
                "Invalid properties for Watchtower",
                TestMethodHelpers.CreateNewWatchtower(),
                TestMethodHelpers.CreateNewLocationDtoAsWatchtower()
           };
            yield return new object[]
           {
                "Invalid properties for WheatMill",
                TestMethodHelpers.CreateNewWheatMill(),
                TestMethodHelpers.CreateNewLocationDtoAsWheatMill()
           };
            yield return new object[]
           {
                "Invalid properties for LumberMill",
                TestMethodHelpers.CreateNewLumberMill(),
                TestMethodHelpers.CreateNewLocationDtoAsLumberMill()
           };
            yield return new object[]
           {
                "Invalid properties for BodyOfWater",
                TestMethodHelpers.CreateNewBodyOfWater(),
                TestMethodHelpers.CreateNewLocationDtoAsBodyOfWater()
           };
            yield return new object[]
           {
                "Invalid properties for InnOrTavern",
                TestMethodHelpers.CreateNewInnOrTavern(),
                TestMethodHelpers.CreateNewLocationDtoAsInnOrTavern()
           };
            yield return new object[]
           {
                "Invalid properties for Temple",
                TestMethodHelpers.CreateNewTemple(),
                TestMethodHelpers.CreateNewLocationDtoAsTemple()
           };
            yield return new object[]
           {
                "Invalid properties for WordWall",
                TestMethodHelpers.CreateNewWordWall(),
                TestMethodHelpers.CreateNewLocationDtoAsWordWall()
           };
            yield return new object[]
           {
                "Invalid properties for Castle",
                TestMethodHelpers.CreateNewCastle(),
                TestMethodHelpers.CreateNewLocationDtoAsCastle()
           };
            yield return new object[]
           {
                "Invalid properties for UnmarkedLocation",
                TestMethodHelpers.CreateNewUnmarkedLocation(),
                TestMethodHelpers.CreateNewLocationDtoAsUnmarkedLocation()
           };
        }

        [Theory]
        [MemberData(nameof(WhiteSpaceProperties))]
        public async void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription_ReturnsExpectedLocation(string description,
            LocationDto createLocationDto, LocationDto formatedCreateLocationDto, 
            Location taskType, Location type)
        {
            // Arrange
            if (type.LocationId == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (type.LocationId == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (type.LocationId == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (type.LocationId == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (type.LocationId == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (type.LocationId == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (type.LocationId == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (type.LocationId == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (type.LocationId == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (type.LocationId == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (type.LocationId == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (type.LocationId == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (type.LocationId == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (type.LocationId == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());
            else if (type.LocationId == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (type.LocationId == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (type.LocationId == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (type.LocationId == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (type.LocationId == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (type.LocationId == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (type.LocationId == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (type.LocationId == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (type.LocationId == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (type.LocationId == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (type.LocationId == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (type.LocationId == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (type.LocationId == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (type.LocationId == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (type.LocationId == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (type.LocationId == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (type.LocationId == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (type.LocationId == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (type.LocationId == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (type.LocationId == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (type.LocationId == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (type.LocationId == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (type.LocationId == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());
            else if (type.LocationId == LocationType.Castle)
                _mockMapper.Setup(x => x.Map<Data.Models.Castle>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCastle());
            else if (type.LocationId == LocationType.GuildHeadquarter)
                _mockMapper.Setup(x => x.Map<GuildHeadquarter>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewGuildHeadquarter());
            else if (type.LocationId == LocationType.UnmarkedLocation)
                _mockMapper.Setup(x => x.Map<UnmarkedLocation>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewUnmarkedLocation());

            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(formatedCreateLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(type.Name, result.Name);
            Assert.Equal(type.Description, result.Description);
            Assert.Equal(type.LocationId, result.LocationId);
        }
        public static IEnumerable<object[]> WhiteSpaceProperties()
        {
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a City with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a City with empty description", new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a City with empty description", 
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Town with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Town with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Town with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Homestead with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Homestead with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Homestead with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Settlement with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Settlement with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Settlement with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DaedricShrine with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DaedricShrine with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DaedricShrine with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a StandingStone with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a StandingStone with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a StandingStone with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Landmark with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Landmark with empty description",
                new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Landmark with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Camp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Camp with empty description", 
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Camp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Cave with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Cave with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Cave with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Clearing with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Clearing with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Clearing with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Dock with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Dock with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Dock with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DragonLair with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DragonLair with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DragonLair with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DwarvenRuin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DwarvenRuin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DwarvenRuin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Farm with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Farm with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Farm with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Fort with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Fort with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Fort with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a GiantCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a GiantCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a GiantCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Grove with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Grove with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Grove with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a ImperialCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a ImperialCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a ImperialCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a LightHouse with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a LightHouse with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a LightHouse with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Mine with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Mine with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Mine with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a NordicTower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a NordicTower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a NordicTower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a OrcStronghold with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a OrcStronghold with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a OrcStronghold with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Pass with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Pass with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Pass with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Ruin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Ruin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Ruin with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Shack with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Shack with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Shack with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Ship with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Ship with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Ship with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Shipwreck with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Shipwreck with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Shipwreck with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Stable with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Stable with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Stable with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a StormcloakCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a StormcloakCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a StormcloakCamp with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Tomb with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Tomb with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Tomb with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Watchtower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Watchtower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Watchtower with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a WheatMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a WheatMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a WheatMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a LumberMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a LumberMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a LumberMill with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a BodyOfWater with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a BodyOfWater with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a BodyOfWater with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a InnOrTavern with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a InnOrTavern with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a InnOrTavern with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Temple with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Temple with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Temple with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a WordWall with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a WordWall with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a WordWall with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Castle with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.Castle,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCastle(),
                    TestMethodHelpers.CreateNewCastle(),
                    TestMethodHelpers.CreateNewCastle()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Castle with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.Castle,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCastle(),
                    TestMethodHelpers.CreateNewCastle(),
                    TestMethodHelpers.CreateNewCastle()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Castle with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.Castle,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsCastle(),
                    TestMethodHelpers.CreateNewCastle(),
                    TestMethodHelpers.CreateNewCastle()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a GuildHeadquarter with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.GuildHeadquarter,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a GuildHeadquarter with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.GuildHeadquarter,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a GuildHeadquarter with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.GuildHeadquarter,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter(),
                    TestMethodHelpers.CreateNewGuildHeadquarter()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a UnmarkedLocation with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.UnmarkedLocation,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a UnmarkedLocation with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        LocationId = LocationType.UnmarkedLocation,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a UnmarkedLocation with empty description",
                    new LocationDto
                    {
                        Name = "Test",
                        Description = "",
                        LocationId = LocationType.UnmarkedLocation,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewLocationDtoAsUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation(),
                    TestMethodHelpers.CreateNewUnmarkedLocation()
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_OrWhiteSpaceProperties))]
        public async void CreateLocationDtoContainsInvalidEmpty_WhiteSpace_OrNullProperties_ReturnsExpectedNull(string description,
            LocationDto createLocationDto, LocationDto badFormatedCreateLocationDto)
        {
            // Arrange
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(badFormatedCreateLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(null, result);
        }
        public static IEnumerable<object[]> UnallowedNull_Invalid_OrWhiteSpaceProperties()
        {
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.City
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Town
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Homestead
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Settlement
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.DaedricShrine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.StandingStone
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Landmark
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Camp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Cave
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Clearing
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Dock
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.DragonLair
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.DwarvenRuin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Farm
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Fort
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.GiantCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Grove
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.ImperialCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.LightHouse
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Mine
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.NordicTower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.OrcStronghold
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Pass
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Ruin
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Shack
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Ship
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Shipwreck
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Stable
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.StormcloakCamp
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Tomb
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Watchtower
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.WheatMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.LumberMill
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.BodyOfWater
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.InnOrTavern
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Temple
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.WordWall
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.Castle
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.GuildHeadquarter
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new LocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        LocationId = LocationType.UnmarkedLocation
                    },
                    (LocationDto)null
            };
        }

        [Fact]
        public async void CreateLocationDtoDoesNotContainValidLocationType_ReturnsNull()
        {
            // Arrange
            var createLocationDto = new LocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(null, result);
        }

        [Fact]
        public async void WhenLocationWithSameNameAndLocationType_AlreadyExists_ReturnsNull()
        {
            // Arrange
            var createLocationDto = TestMethodHelpers.CreateNewLocationDtoAsCity();

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);
            _mockLocationRepository.Setup(x => x.GetLocation()).ReturnsAsync(new List<Location> { TestMethodHelpers.CreateNewCity() });
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCity());

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void WhenLocationWithSameNameAndLocationType_DoesNotExist_ReturnsLocation()
        {
            // Arrange
            var createLocationDto = TestMethodHelpers.CreateNewLocationDtoAsCity();

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<LocationDto>())).Returns(createLocationDto);
            _mockLocationRepository.Setup(x => x.GetLocation()).ReturnsAsync(new List<Location> { TestMethodHelpers.CreateNewTown() });
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<LocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>())).ReturnsAsync(TestMethodHelpers.CreateNewCity());

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(createLocationDto.Name, result.Name);
            Assert.Equal(createLocationDto.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(createLocationDto.LocationId, result.LocationId);
        }
    }

    public class DeleteLocation : LocationDomain_Tests
    {
        [Fact]
        public async void WhenLocationIsFoundAndDeletedSuccessfully_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var location = new City()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City
            };

            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ReturnsAsync(location);
            _mockLocationRepository.Setup(x => x.DeleteLocation(It.IsAny<Location>())).ReturnsAsync(true);

            // Act
            var result = await _locationDomain.DeleteLocation(id);

            // Assert
            Assert.Equal(true, result);
        }

        [Fact]
        public async void WhenLocationIsNotFound_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ReturnsAsync((Location)null);
            _mockLocationRepository.Setup(x => x.DeleteLocation(It.IsAny<Location>())).ReturnsAsync(false);

            // Act
            var result = await _locationDomain.DeleteLocation(id);

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        public async void WhenLocationIsNotFound_DueToError_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            var result = true;
            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            try
            {
                result = await _locationDomain.DeleteLocation(id);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _locationDomain.GetLocation(id));
            Assert.Equal(false, result);
        }

        [Fact]
        public async void WhenLocationIsFound_ButErrorHappensWhenDeleted_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            var result = true;
            var location = new City()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                LocationId = LocationType.City
            };

            _mockLocationRepository.Setup(x => x.GetLocation(It.IsAny<int>())).ReturnsAsync(location);
            _mockLocationRepository.Setup(x => x.DeleteLocation(It.IsAny<Location>())).ThrowsAsync(new Exception());

            // Act
            try
            {
                result = await _locationDomain.DeleteLocation(id);
            }
            catch (Exception)
            {
                result = false;
            }

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _locationDomain.DeleteLocation(id));
            Assert.Equal(false, result);
        }
    }
}
